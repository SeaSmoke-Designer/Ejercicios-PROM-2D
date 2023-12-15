using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class GameManager : NetworkBehaviour
{
    private enum eStates { Ready, Playing, GameOver, SelectMode, Waiting }
    private eStates state;
    private int player1Score;
    private int player2Score;
    [SerializeField]
    private GameObject paddle1Prefab;
    [SerializeField]
    private GameObject paddle2Prefab;

    public int maxScore;

    public TextMeshProUGUI textScorePlayer1;
    public TextMeshProUGUI textScorePlayer2;
    public TextMeshProUGUI textPlayerWin;

    public TextMeshProUGUI messageWin;

    private GameObject ball;
    private GameObject paddle1;
    private GameObject paddle2;

    [SerializeField]
    private GameObject selectModeText;
    private string quienGana;

    //Network

    NetworkVariable<int> netPlayer1Score = new NetworkVariable<int>();
    NetworkVariable<int> netPlayer2Score = new NetworkVariable<int>();
    NetworkVariable<FixedString64Bytes> netQuienGana = new NetworkVariable<FixedString64Bytes>();


    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball");

        state = eStates.SelectMode;
        //paddle2.SetActive(false);
        //paddle2.GetComponent<Paddle>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case eStates.SelectMode:
                UpdateSelectMode();
                break;
            case eStates.Waiting:
                UpdateWaiting();
                break;
            case eStates.Ready:
                UpdateReady();
                break;
            case eStates.Playing:
                UpdatePlaying();
                break;
            case eStates.GameOver:
                UpdateGameOver();
                break;

        }
    }

    public void Player1Scores()
    {
        player1Score++;
        netPlayer1Score.Value++;
        PlayerScore("Player1");
    }

    public void Player2Scores()
    {
        player2Score++;
        netPlayer2Score.Value++;
        PlayerScore("Player2");
    }

    private void PlayerScore(string player)
    {
        RellenarMarcador();
        ResetPaddles();
        if (player.Equals("Player1"))
        {
            paddle2.GetComponent<Paddle>().IncreasePaddle();
            if (player1Score > 1)
                paddle1.GetComponent<Paddle>().ShortenPaddle();
        }
        else
        {
            paddle1.GetComponent<Paddle>().IncreasePaddle();
            if (player2Score > 1)
                paddle2.GetComponent<Paddle>().ShortenPaddle();
        }
        ball.GetComponent<Ball>().Reset();
        state = eStates.Ready;
        ReadyClientRpc();
        if (PlayerWin())
        {
            state = eStates.GameOver;
            GameOverClientRpc();
        }
    }

    public void ResetPaddles()
    {
        paddle1.GetComponent<Paddle>().Reset();
        paddle2.GetComponent<Paddle>().Reset();
    }

    public void ResetScore()
    {
        //if (PlayerWin())
        //{
        textPlayerWin.enabled = false;
        messageWin.enabled = false;
        //}
        player1Score = 0;
        player2Score = 0;
        if (!IsServer && !IsClient)
        {
            paddle1.GetComponent<Paddle>().ResetSize();
            paddle2.GetComponent<Paddle>().ResetSize();
        }
        RellenarMarcador();
        Debug.Log("Marcador reseteado...");
    }

    public void RellenarMarcador()
    {
        textScorePlayer1.SetText(player1Score.ToString());
        textScorePlayer2.SetText(player2Score.ToString());
    }

    public bool PlayerWin()
    {
        if (player1Score == maxScore)
        {
            quienGana = "Player 1 ha ganado!";
            netQuienGana.Value = quienGana;
            textPlayerWin.SetText(quienGana);
            return true;
        }
        else if (player2Score == maxScore)
        {
            quienGana = "Player 2 ha ganado!";
            netQuienGana.Value = quienGana;
            textPlayerWin.SetText(quienGana);
            return true;
        }
        return false;
    }

    void UpdateSelectMode()
    {
        if (Input.GetKeyDown("1"))
        {
            InstanciarPalas();
            paddle2.GetComponent<Paddle>().enabled = true;
            paddle2.GetComponent<AutoPaddle>().enabled = false;
            selectModeText.SetActive(false);
            state = eStates.Ready;
        }
        else if (Input.GetKeyDown("2"))
        {
            InstanciarPalas();
            paddle2.GetComponent<Paddle>().enabled = false;
            paddle2.GetComponent<AutoPaddle>().enabled = true;
            selectModeText.SetActive(false);
            state = eStates.Ready;
        }
        else if (Input.GetKeyDown("3"))
        {
            NetworkManager.Singleton.StartHost();
            //InstanciarPalasNet();
            paddle1 = Instantiate(paddle1Prefab, new Vector3(-8, 0, 0), Quaternion.identity);
            paddle1.GetComponent<NetworkObject>().Spawn(true);
            selectModeText.SetActive(false);
            state = eStates.Waiting;
        }
        else if (Input.GetKeyDown("4"))
        {
            NetworkManager.Singleton.StartClient();
            selectModeText.SetActive(false);
            state = eStates.Ready;
        }
    }



    void UpdateReady()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (IsClient && IsHost)
            {
                ball.GetComponent<Ball>().Launch();
                StarGameClientRpc();
            }
            else
                StarGameServerRpc();

            state = eStates.Playing;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            if (ScoreChanged())
                ResetScore();
        }
    }

    void UpdatePlaying()
    {
        /*if (PlayerWin())
            state = eStates.GameOver;*/
    }
    void UpdateWaiting()
    {
        if (paddle2 != null)
            state = eStates.Ready;
    }

    void UpdateGameOver()
    {
        textPlayerWin.enabled = true;
        Debug.Log(state.ToString());
        if (!IsServer)
        {
            textPlayerWin.SetText(quienGana);
            messageWin.SetText("Esperando respuesta del Host...");
        }
        messageWin.enabled = true;

        if (IsClient && IsServer)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                SelectModeClientRpc();
                ResetScore();
                selectModeText.SetActive(true);
                state = eStates.SelectMode;
                NetworkManager.Singleton.Shutdown();
            }
        }

    }
    void DestruirPalas()
    {
        Destroy(paddle1);
        Destroy(paddle2);
    }

    bool ScoreChanged()
    {
        if (player1Score > 0 || player2Score > 0)
            return true;
        else
            return false;
    }

    void InstanciarPalas()
    {
        paddle1 = Instantiate(paddle1Prefab);
        paddle2 = Instantiate(paddle2Prefab);
    }

    public override void OnNetworkSpawn()
    {
        if (!IsServer)
        {
            netPlayer1Score.OnValueChanged += OnScoreChanged;
            netPlayer2Score.OnValueChanged += OnScoreChanged;
            netQuienGana.OnValueChanged += OnWinnerChanged;
            //netQuienGana.OnValueChanged += OnScoreChanged;
            SpawnClientPlayerServerRpc(NetworkManager.Singleton.LocalClientId);

        }
    }

    public override void OnNetworkDespawn()
    {
        if (!IsServer)
        {
            netPlayer1Score.OnValueChanged -= OnScoreChanged;
            netPlayer2Score.OnValueChanged -= OnScoreChanged;
            netQuienGana.OnValueChanged -= OnWinnerChanged;
            //netQuienGana.OnValueChanged -= OnScoreChanged;
        }
    }


    public void OnWinnerChanged(FixedString64Bytes previous, FixedString64Bytes current)
    {
        //textScorePlayer1.text = player1Score.ToString();
        //textScorePlayer2.text = player2Score.ToString();
        textPlayerWin.SetText(netQuienGana.Value.ToString());
        Debug.Log(netQuienGana.Value);


    }

    public void OnScoreChanged(int previous, int current)
    {
        //textScorePlayer1.text = player1Score.ToString();
        //textScorePlayer2.text = player2Score.ToString();
        textScorePlayer1.SetText(netPlayer1Score.Value.ToString());
        textScorePlayer2.SetText(netPlayer2Score.Value.ToString());
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnClientPlayerServerRpc(ulong clientId)
    {
        paddle2 = Instantiate(paddle1, new Vector3(8, 0, 0), Quaternion.identity);
        paddle2.GetComponent<NetworkObject>().SpawnWithOwnership(clientId, true);
        paddle2.tag = "Player2";
    }

    [ClientRpc]
    void StarGameClientRpc()
    {
        state = eStates.Playing;
    }

    [ServerRpc(RequireOwnership = false)]
    void StarGameServerRpc()
    {
        ball.GetComponent<Ball>().Launch();
        state = eStates.Playing;
    }

    [ClientRpc]
    void ReadyClientRpc()
    {
        state = eStates.Ready;
    }

    [ClientRpc]
    void GameOverClientRpc()
    {
        state = eStates.GameOver;
    }

    [ClientRpc]
    void SelectModeClientRpc()
    {
        ResetScore();
        selectModeText.SetActive(true);
        state = eStates.SelectMode;
    }
}

using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    //Network

    NetworkVariable<int> netPlayer1Score = new NetworkVariable<int>();
    NetworkVariable<int> netPlayer2Score = new NetworkVariable<int>();


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
        PlayerScore("Player1");
    }

    public void Player2Scores()
    {
        player2Score++;
        PlayerScore("Player2");
    }

    private void PlayerScore(string player)
    {
        RellenarMarcador();
        //string msjPlayer = player == "Player1" ? "Player 1 " : "Player 2";
        //Debug.Log(msjPlayer + " ha marcado gol! Marcador: " + player1Score + " : " + player2Score);
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
            textPlayerWin.enabled = true;
            messageWin.enabled = true;
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
        if (PlayerWin())
        {
            textPlayerWin.enabled = false;
            messageWin.enabled = false;
        }
        player1Score = 0;
        player2Score = 0;
        paddle1.GetComponent<Paddle>().ResetSize();
        paddle2.GetComponent<Paddle>().ResetSize();
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
            textPlayerWin.SetText("Player 1 ha ganado!");
            return true;
        }
        else if (player2Score == maxScore)
        {
            textPlayerWin.SetText("Player 2 ha ganado!");
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
        if (Input.GetKeyDown(KeyCode.C))
        {
            ResetScore();
            selectModeText.SetActive(true);
            state = eStates.SelectMode;
        }
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
            SpawnClientPlayerServerRpc(NetworkManager.Singleton.LocalClientId);

        }
    }

    public override void OnNetworkDespawn()
    {
        if (!IsServer)
        {
            netPlayer1Score.OnValueChanged -= OnScoreChanged;
            netPlayer2Score.OnValueChanged -= OnScoreChanged;
        }
    }

    public void OnScoreChanged(int previous, int current)
    {
        textScorePlayer1.text = player1Score.ToString();
        textScorePlayer2.text = player2Score.ToString();
    }

    [ServerRpc(RequireOwnership = false)]
    void SpawnClientPlayerServerRpc(ulong clientId)
    {
        paddle2 = Instantiate(paddle1, new Vector3(8, 0, 0), Quaternion.identity);
        paddle2.GetComponent<NetworkObject>().SpawnWithOwnership(clientId, true);
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
        state = eStates.Ready;
    }

}

using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int player1Score;
    private int player2Score;

    public int maxScore;

    public TextMeshProUGUI scorePLayer1;
    public TextMeshProUGUI scorePLayer2;
    public TextMeshProUGUI textPlayerWin;

    public TextMeshProUGUI messageWin;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Player1Scores(){
        player1Score++;
        PlayerScore("Player1");
    }

    public void Player2Scores(){
        player2Score++;
        PlayerScore("Player2");
    }

    private void PlayerScore(string player){
        RellenarMarcador();
        string msjPlayer = player == "Player1" ? "Player 1 " : "Player 2";
        Debug.Log(msjPlayer + " ha marcado gol! Marcador: " + player1Score + " : " + player2Score);
        ResetPaddles();

        if (player.Equals("Player1"))
        {
            GameObject.Find("Player2").GetComponent<Paddle>().IncreasePaddle();
            if (player1Score > 1)
                GameObject.Find("Player1").GetComponent<Paddle>().ShortenPaddle();
        }
        else
        {
            GameObject.Find("Player1").GetComponent<Paddle>().IncreasePaddle();
            if (player2Score > 1)
                GameObject.Find("Player2").GetComponent<Paddle>().ShortenPaddle();
        }
            

        GameObject.Find("Ball").GetComponent<Ball>().Reset();
        if (PlayerWin())
        {
            textPlayerWin.enabled = true;
            messageWin.enabled = true;
        }
    }

    public void ResetPaddles()
    {
        GameObject.Find("Player1").GetComponent<Paddle>().Reset();
        GameObject.Find("Player2").GetComponent<Paddle>().Reset();
    }

    public void ResetScore(){
        if(PlayerWin()){
            textPlayerWin.enabled = false;
            messageWin.enabled = false;
        }
        player1Score = 0;
        player2Score = 0;
        GameObject.Find("Player1").GetComponent<Paddle>().ResetSize();
        GameObject.Find("Player2").GetComponent<Paddle>().ResetSize();
        RellenarMarcador();
        Debug.Log("Marcador reseteado...");
    }

    public void RellenarMarcador(){
        scorePLayer1.SetText(player1Score.ToString());
        scorePLayer2.SetText(player2Score.ToString());
    }

    public bool PlayerWin(){
        if(player1Score==maxScore){
            textPlayerWin.SetText("Player 1 ha ganado!");
            return true;
        } 
        else if(player2Score==maxScore){
            textPlayerWin.SetText("PLayer 2 ha ganado!");
            return true;
        }
        return false;
    }

   
}

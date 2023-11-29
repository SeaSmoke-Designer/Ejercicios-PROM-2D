using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField]
    //private GameObject ball;
    private enum States { Ready, Playing, GameOver };
    private States state;

    [SerializeField]
    private GameObject ballPrefab;
    private GameObject ball;
    private int vidas;
    void Start()
    {
        state = States.Ready;
        ball = Instantiate(ballPrefab);
        //ball = GameObject.Find("Ball");
        vidas = 3;
    }



    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case States.Ready:
                UpdateReady();
                break;
            case States.Playing:
                UpdatePlaying();
                break;
            case States.GameOver:
                UpdateGameOver();
                break;
        }

    }


    void UpdateReady()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ball.GetComponent<Ball>().Launch();
            state = States.Playing;
        }
    }

    void UpdatePlaying()
    {
        if (vidas == 0)
        {
            state = States.GameOver;
        }
    }

    void UpdateGameOver()
    {

    }

    public void ResetBall()
    {
        Destroy(ball);
        if (vidas > 0)
        {
            vidas -= 1;
            ball = Instantiate(ballPrefab);
            state = States.Ready;
        }
    }
}

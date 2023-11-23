using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ball;
    private enum States { Ready, Playing, GameOver };
    private States state;
    void Start()
    {
        state = States.Ready;
        ball = GameObject.Find("Ball");
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
                break;
            case States.GameOver:
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
}

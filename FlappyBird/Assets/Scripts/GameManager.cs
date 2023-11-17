using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum States { Playing, Pause, Prepared, GameOver };
    private States state;

    [SerializeField]
    GameObject birdPrefab;
    private GameObject bird;

    [SerializeField]
    GameObject mainTitle;

    [SerializeField]
    GameObject deadTitle;

    [SerializeField]
    GameObject scoreText;

    [SerializeField]
    TextMeshProUGUI scorePlayingText;

    [SerializeField]
    TextMeshProUGUI scoreFinishText;

    private int score;

    private GameObject spawner;
    private GameObject scroller;

    // Start is called before the first frame update
    void Start()
    {
        state = States.Prepared;
        deadTitle.SetActive(false);
        scoreText.SetActive(false);
        spawner = GameObject.Find("Spawner");
        scroller = GameObject.Find("Scroller");
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case States.Prepared:
                Prepared();
                break;
            case States.Playing:
                Playing();
                break;
            case States.Pause:
                Pause();
                break;
            case States.GameOver:
                GameOver();
                break;

        }

    }

    void Prepared()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainTitle.SetActive(false);
            scoreText.SetActive(true);
            bird = Instantiate(birdPrefab);
            spawner.GetComponent<Spawner>().SpawnPipe();
            state = States.Playing;
        }
    }

    void Playing()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            state = States.Pause;
        }
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            state = States.Playing;
        }
    }

    void GameOver()
    {
        deadTitle.SetActive(true);
        scoreText.SetActive(false);
        scroller.GetComponent<Scroller>().DestroyPipes();
        spawner.GetComponent<Spawner>().DestroyPipes();
        scoreFinishText.SetText($"Has conseguido {score} puntos");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            state = States.Playing;
            deadTitle.SetActive(false);
            scoreText.SetActive(true);
            bird = Instantiate(birdPrefab);
            spawner.GetComponent<Spawner>().SpawnPipe();
            score = 0;
            scorePlayingText.SetText(score.ToString());
        }
    }

    public void KillBird()
    {
        GameObject.Destroy(bird);
        state = States.GameOver;
    }

    public void Point()
    {
        score++;
        scorePlayingText.SetText(score.ToString());
    }


}

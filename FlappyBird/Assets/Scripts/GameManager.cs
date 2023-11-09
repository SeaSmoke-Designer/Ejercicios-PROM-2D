using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum States {Playing, Pause, Prepared, GameOver};
    private States state;

    [SerializeField]
    GameObject birdPrefab;
    private GameObject bird;


    [SerializeField]
    TextMeshProUGUI startText;
    
    [SerializeField]
    GameObject mainTitle;

    private GameObject spawner;
    private GameObject scroller; 

    // Start is called before the first frame update
    void Start()
    {
        //isPlaying = false;
        state = States.Prepared;
        spawner = GameObject.Find("Spawner");
        scroller = GameObject.Find("Scroller");
    }

    // Update is called once per frame
    void Update()
    {
        /*if(!isPlaying){
            if(Input.GetKeyDown(KeyCode.Space)){
                startText.enabled = false;
                bird = Instantiate(birdPrefab);
                Debug.Log("Empieza el juego");
                isPlaying = true;
        }
        }*/
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
        //if(state == States.Prepared){
            
        //}
        
    }

    void Prepared(){
        if(Input.GetKeyDown(KeyCode.Space)){
                startText.enabled = false;
                
                bird = Instantiate(birdPrefab);
                
                //Debug.Log("Empieza el juego");
                spawner.GetComponent<Spawner>().SpawnPipe();
                //isPlaying = true;
                state = States.Playing;
                }
    }

    void Playing() {
        if (Input.GetKeyDown(KeyCode.P))
        {
            state = States.Pause;
        }
    }

    void Pause(){
        Time.timeScale = 0f;
        if(Input.GetKeyDown(KeyCode.P)){
            state = States.Playing;
            Time.timeScale = 1f;
            
        }
    }

    void GameOver(){
        scroller.GetComponent<Scroller>().DestroyPipes();
        spawner.GetComponent<Spawner>().DestroyPipes();
    }

    public void KillBird(){
        GameObject.Destroy(bird);
        state = States.GameOver;
    }
}

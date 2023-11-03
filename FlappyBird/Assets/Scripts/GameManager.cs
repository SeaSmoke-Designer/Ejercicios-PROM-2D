using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private enum States {Playing, Pause, Stop};
    private States state;

    [SerializeField]
    GameObject birdPrefab;
    private GameObject bird;


    [SerializeField]
    TextMeshProUGUI startText;
    private bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
        state = States.Stop;
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

        if(state == States.Stop){
            if(Input.GetKeyDown(KeyCode.Space)){
                startText.enabled = false;
                bird = Instantiate(birdPrefab);
                Debug.Log("Empieza el juego");
                isPlaying = true;
                state = States.Playing;
                }
        }
        
    }

    public void KillBird(){
        GameObject.Destroy(bird);
    }
}

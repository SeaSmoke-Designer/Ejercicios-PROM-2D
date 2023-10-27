using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject birdPrefab;

    private GameObject bird;

    public TextMeshProUGUI startText;
    private bool isPlaying;

    // Start is called before the first frame update
    void Start()
    {
        isPlaying = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isPlaying){
            if(Input.GetKeyDown(KeyCode.Space)){
                startText.enabled = false;
                bird = Instantiate(birdPrefab);
                Debug.Log("Empieza el juego");
                isPlaying = true;
        }
        }
        
    }

    public void KillBird(){
        GameObject.Destroy(bird);
    }
}

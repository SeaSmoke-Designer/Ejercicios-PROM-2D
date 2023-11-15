using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPause;

    [SerializeField]
    GameObject textPause;
    //private GameObject manager;
    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        //manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPause)
            Pause();
        else
            QuitPause();
    }


    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P) || isPause)
        {
            Time.timeScale = 0f;
            isPause = true;
        }
    }

    void QuitPause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1f;
            isPause = false;
            //manager.GetComponent<GameManager>().QuitPause();
        }
    }
}

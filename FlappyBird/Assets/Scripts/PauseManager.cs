using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPause;

    [SerializeField]
    GameObject textPause;

    [SerializeField]
    AudioClip pauseAudio;
    //private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        isPause = false;
        textPause.SetActive(false);
        //gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (gameManager.IsPlaying())
        //{
            if (!isPause)
                Pause();
            else
                QuitPause();
        //}
    }

    void Pause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0f;
            AudioManager.Instance.PlayClip(pauseAudio);
            isPause = true;
            textPause.SetActive(true);
        }
    }

    void QuitPause()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 1f;
            AudioManager.Instance.PlayClip(pauseAudio);
            isPause = false;
            textPause.SetActive(false);
        }
    }
}

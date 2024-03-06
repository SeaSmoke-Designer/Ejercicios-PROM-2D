using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    private UserDataManager userDataManager;
    private void Awake()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayFinalFantasyMusic();
        userDataManager = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
            EmpezarOtraVez();
    }

    void EmpezarOtraVez()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.StartAgain();
        userDataManager.ResetValues();
        SceneManager.LoadScene(1);
    }
}

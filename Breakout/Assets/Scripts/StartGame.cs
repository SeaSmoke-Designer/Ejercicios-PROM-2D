using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private UserDataManager userDataManager;
    // Start is called before the first frame update
    void Start()
    {
        //SceneManager.LoadScene(0);
        userDataManager = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();
        userDataManager.ResetValues();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public void CargarLevelPrecreados()
    {
        userDataManager.levelProcedural = false;
        SceneManager.LoadScene(2);
    }
    public void CargarLevelProcedurales()
    {
        userDataManager.levelProcedural = true;
        SceneManager.LoadScene(1);
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum eStates { Ready, Playing, GameOver, NextLevel };
    private eStates state;

    [SerializeField]
    private GameObject ballPrefab;
    private GameObject ball;
    [SerializeField]
    private GameObject paddlePrefab;
    private GameObject paddle;
    [SerializeField]
    private GameObject textLose;
    private List<GameObject> ladrillosList = new List<GameObject>();

    [SerializeField]
    private List<GameObject> spritesLifes = new List<GameObject>();

    [SerializeField]
    private Sprite spriteNoLife;
    [SerializeField]
    private GameObject textWin;
    private int vidas;
    void Start()
    {
        state = eStates.Ready;
        paddle = Instantiate(paddlePrefab);
        ball = Instantiate(ballPrefab);
        textLose.SetActive(false);
        textWin.SetActive(false);
        vidas = 3;
    }



    // Update is called once per frame
    void Update()
    {

        switch (state)
        {
            case eStates.Ready:
                UpdateReady();
                break;
            case eStates.Playing:
                UpdatePlaying();
                break;
            case eStates.GameOver:
                UpdateGameOver();
                break;
            case eStates.NextLevel:
                UpdateNextLevel();
                break;
        }

    }


    void UpdateReady()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ball.GetComponent<Ball>().Launch();
            state = eStates.Playing;
        }
    }

    void UpdatePlaying()
    {
        if (Win())
            state = eStates.NextLevel; 
    }
    void UpdateNextLevel()
    {
        DestroyObjects();
        textWin.SetActive(true);
        if(Input.GetKeyDown(KeyCode.Space))
            SceneManager.LoadScene(2);
    }

    void UpdateGameOver()
    {
        DestroyObjects();
        textLose.SetActive(true);
        if (Input.GetKeyDown(KeyCode.R))
            RestarGame();
        if (Input.GetKeyDown(KeyCode.Escape))
            QuitGame();
    }

    public void ResetBall()
    {
        Destroy(ball);
        QuitarVida();
    }
    void QuitarVida()
    {
        vidas -= 1;
        CambiarSprites();
        if (vidas > 0)
        {
            ball = Instantiate(ballPrefab);
            state = eStates.Ready;
        }
        else
            state = eStates.GameOver;
    }
    void CambiarSprites()
    {
        switch (vidas)
        {
            case 2:
                spritesLifes[2].GetComponent<SpriteRenderer>().sprite = spriteNoLife;
                break;
            case 1:
                spritesLifes[1].GetComponent<SpriteRenderer>().sprite = spriteNoLife;
                break;
            case 0:
                spritesLifes[0].GetComponent<SpriteRenderer>().sprite = spriteNoLife;
                break;
        }
    }

    public GameObject PasarPaddle()
    {
        if (paddle is not null)
            return paddle;
        else
            return null;
    }
    public void LlenarListaLadrillos(GameObject gameObject)
    {
        if (gameObject is not null)
            ladrillosList.Add(gameObject);
    }
    public void EliminarLadrillo(GameObject gameObject)
    {
        for (int i = 0; i < ladrillosList.Count; i++)
        {
            if (ladrillosList[i] == gameObject)
                ladrillosList.Remove(gameObject);
        }

    }
    void DestroyObjects()
    {
        if(spritesLifes.Count != 0)
        {
            Destroy(ball);
            Destroy(paddle);
            if (ladrillosList.Count > 0)
                foreach (GameObject item in ladrillosList)
                    Destroy(item);

            ladrillosList.Clear();
            foreach (GameObject item in spritesLifes)
                Destroy(item);
            spritesLifes.Clear();
        }
       
    }

    bool Win()
    {
        if (ladrillosList.Count > 0)
            return false;
        else
            return true;
    }
    void RestarGame()
    {
        SceneManager.LoadScene(0);
    }
    void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}

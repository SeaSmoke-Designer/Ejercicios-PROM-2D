using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private enum eStates { Ready, Playing, GameOver };
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
    //[SerializeField]
    //private SpriteRenderer spriteRenderer;
    private int vidas;
    void Start()
    {
        state = eStates.Ready;
        paddle = Instantiate(paddlePrefab);
        ball = Instantiate(ballPrefab);
        textLose.SetActive(false);

        //ball = GameObject.Find("Ball");
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
            {
                Debug.Log("Elimina");
                ladrillosList.Remove(gameObject);
            }

        }

    }
    //Revisar y poner condicion porque en el update estramos todo el rato aqui 
    void DestroyObjects()
    {
        Destroy(ball);
        Destroy(paddle);
        if (ladrillosList.Count > 0)
            foreach (GameObject item in ladrillosList)
                Destroy(item);

        ladrillosList.Clear();
        foreach (GameObject item in spritesLifes)
        {
            Destroy(item);
        }
        /*for (int i = 0; i < spritesLifes.Count; i++)
        {
            Destroy(spritesLifes[i]);
        }*/
        spritesLifes.Clear();
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

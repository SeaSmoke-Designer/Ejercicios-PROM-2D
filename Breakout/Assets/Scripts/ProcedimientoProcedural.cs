using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ProcedimientoProcedural : MonoBehaviour
{
    [SerializeField] private List<GameObject> ladrillosPrefabs;
    private GameObject ladrilloPrefab;
    //[SerializeField] private float maxFilas;
    private int filas;
    private int columnas;

    private float maxLadrillos = 8;

    [SerializeField] private int nivel;
    private bool nivelGenerado;
    private int colorLadrillo;
    private UserDataManager userDataManager;

    [SerializeField] private float maxY; //3.5f
    [SerializeField] private float minY; //-1.5f
    [SerializeField] private float maxX; //7
    [SerializeField] private float minX; //-7.1

    private float posicionEnX;
    private float posicionEnY;

    private int vidaLadrillo;

    //private float num;
    //private float num2;

    //10x6 es el maximo. 10 columnas y 6 filas.
    //3x2 es el minimo. 3 columnas y 2 filas.
    //6x3
    //8x4
    //9x5
    //10x6

    //Aclaracion, el orden de arriba quiere decir lo siguiente: Cada nivel puede generar 3 columnas y 2 filas por ejemplo, asi sucesivamente 

    private void Awake()
    {
        userDataManager = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();

    }

    // Start is called before the first frame update
    void Start()
    {
        nivel = userDataManager.level;
        MirarNivel();
        SpwanLadrillos();
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
        //NextLevel();
    }

    void ElegirColorYVida()
    {
        //do
        //{
        colorLadrillo = Random.Range(0, 5);
        if (nivel >= 8)
            vidaLadrillo = Random.Range(3, 5);
        else
            vidaLadrillo = Random.Range(1, 5);
        //Debug.Log(vidaLadrillo);
        //Debug.Log(colorLadrillo);
        //} while ();

    }
    void ElegirPosicionEnY()
    {
        posicionEnY = Random.Range(minY, maxY);
    }
    void ElegirPosicionEnX()
    {

    }

    void MirarNivel()
    {
        switch (nivel)
        {
            case 1:
                columnas = 3;
                filas = 2;
                break;
            case 3:
                columnas = 6;
                filas = 3;
                //6x3
                break;
            case 5:
                columnas = 8;
                filas = 4;
                //8x4
                break;
            case 8:
                columnas = 9;
                filas = 5;
                //9x5
                break;
            case 10:
                columnas = 10;
                filas = 6;
                //10x6
                break;
            default:
                columnas = userDataManager.columnas;
                filas = userDataManager.filas;
                break;
        }
        userDataManager.CambiarColumnasFilas(columnas, filas);
    }

    void SpwanLadrillos()
    {
        for (int i = 0; i < filas; i++)
        {
            ElegirColorYVida();

            float lerpYValue = i / (filas - 1f);
            float yPos = Mathf.Lerp(minY, maxY, lerpYValue);
            //Debug.Log(yPos);
            for (int j = 0; j < columnas; j++)
            {

                float lerpXValue = j / (columnas - 1f);
                float xPos = Mathf.Lerp(-7.1f, 7, lerpXValue);

                GameObject ladrillo = Instantiate(ladrillosPrefabs[colorLadrillo]);
                ladrillo.GetComponent<GestionarLadrillos>().SetVidaLadrillo(vidaLadrillo);
                ladrillo.transform.position = new Vector2(xPos, yPos);
                Debug.Log("Tag: " + ladrillo.tag + " Vida: " + vidaLadrillo);

            }
        }
    }

    void NextLevel()
    {
        userDataManager.level++;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

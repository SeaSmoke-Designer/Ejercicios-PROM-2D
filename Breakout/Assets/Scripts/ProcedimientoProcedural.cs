using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProcedimientoProcedural : MonoBehaviour
{
    [SerializeField] private List<GameObject> ladrillosPrefabs;
    private GameObject ladrilloPrefab;
    //[SerializeField] private float maxFilas;
    private float filas;
    private float columnas;

    private float maxLadrillos = 8;

    private int nivel;
    private bool nivelGenerado;
    private int colorLadrillo;

    [SerializeField] private float maxY; //3.5f
    [SerializeField] private float minY; //-1.5f
    [SerializeField] private float maxX; //7
    [SerializeField] private float minX; //-7.1

    private float posicionEnX;
    private float posicionEnY;

    //10x6 es el maximo. 10 columnas y 6 filas.
    //3x2 es el minimo. 3 columnas y 2 filas.
    //6x3
    //8x4
    //9x5
    //10x6

    //Aclaracion, el orden de arriba quiere decir lo siguiente: Cada nivel puede generar 3 columnas y 2 filas por ejemplo, asi sucesivamente 

    // Start is called before the first frame update
    void Start()
    {
        nivel = 1;
        filas = 2;
        columnas = 3;

        for (int i = 0; i < filas; i++)
        {
            ElegirColor();
            for (int j = 0; j < columnas; j++)
            {
                Instantiate(ladrillosPrefabs[colorLadrillo]);
            }//Aplicar el Lerp
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (nivel)
        {
            //Level 1 empieza con lo minimo
            case 3:
                //6x3
                break;
            case 5:
                //8x4
                break;
            case 8:
                //9x5
                break;
            case 10:
                //10x6
                break;

        }
    }

    void ElegirColor()
    {
        //do
        //{
        colorLadrillo = Random.Range(0, 6);
        //} while ();

    }
    void ElegirPosicionEnY()
    {
        posicionEnY = Random.Range(minY, maxY);
    }
    void ElegirPosicionEnX()
    {

    }
}

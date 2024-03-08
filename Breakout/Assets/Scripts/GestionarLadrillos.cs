using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;

public class GestionarLadrillos : MonoBehaviour
{
    [SerializeField] private List<Sprite> spritesBlue;
    [SerializeField] private List<Sprite> spritesRed;
    [SerializeField] private List<Sprite> spritesYellow;
    [SerializeField] private List<Sprite> spritesGreen;
    [SerializeField] private List<Sprite> spritesViolet;
    //[Range(1, 4)]
    //[SerializeField] 
    private int vidaLadrillo;

    private SpriteRenderer spriteRenderer;

    [Range(0, 1)]
    [SerializeField] private float probabilidadPowerUp;

    [SerializeField] private GameObject powerUpBolaPrefab;
    [SerializeField] private GameObject powerUpVidaPrefab;
    private GameObject powerUpBola;
    private GameObject powerUpVida;
    private GameManager gm;
    private UserDataManager userDataManager;
    private bool procedural;

    [SerializeField] bool aleatorio;

    //[SerializeField] private TagValueType tagLadrillo;
    // Start is called before the first frame update

    private void Awake()
    {
        userDataManager = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();
        if (userDataManager != null) procedural = userDataManager.levelProcedural;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        gm.LlenarListaLadrillos(gameObject);
    }
    void Start()
    {
        //vidaLadrillo = 1;
        if (aleatorio)
            ElegirTipoAleatorio();
        else
            ElegirTipo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ElegirTipo()
    {
        if (!procedural)
            vidaLadrillo = Random.Range(1, 5);

        SpritesLadrillos();
    }

    void ElegirTipoAleatorio()
    {
        vidaLadrillo = Random.Range(1, 5);

    }

    void SpritesLadrillos()
    {
        switch (gameObject.tag)
        {
            case "Blue":
                spriteRenderer.sprite = spritesBlue[vidaLadrillo - 1];
                break;
            case "Red":
                spriteRenderer.sprite = spritesRed[vidaLadrillo - 1];
                break;
            case "Yellow":
                spriteRenderer.sprite = spritesYellow[vidaLadrillo - 1];
                break;
            case "Violet":
                spriteRenderer.sprite = spritesViolet[vidaLadrillo - 1];
                break;
            case "Green":
                spriteRenderer.sprite = spritesGreen[vidaLadrillo - 1];
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
            QuitarVida();
    }

    void QuitarVida()
    {
        vidaLadrillo -= 1;
        if (vidaLadrillo > 0)
        {
            ElegirTipo();
        }
        else
        {
            LanzarPowerUp();
            //gm.EliminarLadrillo(gameObject);
            Destroy(gameObject);
        }
    }



    void LanzarPowerUp()
    {
        if (gameObject.CompareTag("Blue"))
        {
            if (ProbabilidadPowerUp())
            {
                powerUpBola = Instantiate(powerUpBolaPrefab);
                powerUpBola.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
            }
        }
        else if (gameObject.CompareTag("Red"))
        {
            if (ProbabilidadPowerUp())
            {
                powerUpVida = Instantiate(powerUpVidaPrefab);
                powerUpVida.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
            }
        }
    }


    bool ProbabilidadPowerUp() => Random.value < probabilidadPowerUp;

    public void SetVidaLadrillo(int value) => vidaLadrillo = value;


    /*void CompribarVida()
    {
        spriteRenderer =
        switch (vidaLadrillo)
        {
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;

            default:
        }
    }*/



}

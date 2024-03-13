using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using System.Text;

public class GestionarLadrillos : MonoBehaviour
{
    [SerializeField] private List<Sprite> spritesBlue;
    [SerializeField] private List<Sprite> spritesRed;
    [SerializeField] private List<Sprite> spritesYellow;
    [SerializeField] private List<Sprite> spritesGreen;
    [SerializeField] private List<Sprite> spritesViolet;
    [Range(1, 4)]
    [SerializeField]
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
    private readonly List<string> tagsLadrillos = new List<string> { "Blue", "Red", "Yellow", "Violet", "Green" };
    //private StringBuilder sb;

    //[SerializeField] private TagValueType tagLadrillo;
    // Start is called before the first frame update

    private void Awake()
    {
        //tagsLadrillos = new string[] {"Blue","Red","Yellow","Violet","Green"};
        userDataManager = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();
        if (userDataManager != null) procedural = userDataManager.levelProcedural;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }
    void Start()
    {
        if (procedural)
        {
            //ElegirTipoProcedural();
            SpritesLadrillos();
        }
        else
        {
            if (aleatorio)
                ElegirTipoAleatorio();
            else
                SpritesLadrillos();
        }
        gm.LlenarListaLadrillos(gameObject);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ElegirTipoAleatorio()
    {
        vidaLadrillo = Random.Range(1, 5);
        gameObject.tag = tagsLadrillos[Random.Range(0, 5)];
        SpritesLadrillos();
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
        {
            //sb.Append("Nombre ladrillo: " + gameObject.tag + "\n");
            //Debug.Log("Nombre ladrillo: " + gameObject.tag);
            QuitarVida();
        }

    }

    void QuitarVida()
    {
        //sb.Append("Vida anterior: " + vidaLadrillo + "\n");
        vidaLadrillo--;
        if (vidaLadrillo > 0)
        {
            SpritesLadrillos();
            //sb.Append("Vida Actual: " + vidaLadrillo + "\n");
        }
        else
        {
            LanzarPowerUp();
            gm.EliminarLadrillo(gameObject);
            Destroy(gameObject);
            //sb.Append("Fui Destruido");
        }
        //Debug.Log(sb);
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

}

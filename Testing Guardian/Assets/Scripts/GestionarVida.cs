using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GestionarVida : MonoBehaviour
{
    [SerializeField] private float vidaMaxima;
    private float vidaActual;

    [SerializeField] private TextMeshProUGUI vidaText;
    [SerializeField] private Slider slider;
    [SerializeField] private bool enemigoCerca;

    [SerializeField] private float velocidadRegeneracion;
    [SerializeField] private Image backgroundBarra;
    [SerializeField] private Image frontBarra;
    private float transparencia;
    private bool stopRegenerar;
    [SerializeField] private float tiempoStun;
    private bool estaStun;
    private bool estaEnvenenado;

    private float veneno = 0.5f;

    private float duracionVeneno = 5f;

    private Coroutine coroutineActual;

    void Awake()
    {
        vidaActual = vidaMaxima;
        vidaText.text = vidaMaxima.ToString();
        //slider = GetComponent<Slider>();
        slider.maxValue = vidaMaxima;
        slider.value = vidaMaxima;
        transparencia = 1f;
        estaStun = false;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (vidaActual <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
            stopRegenerar = true;
            StartCoroutine(CorEsperaregenerar());
        }
        if (vidaActual < 100)
            RegenerarVida();

        vidaText.text = vidaActual.ToString();
        slider.value = vidaActual;
        /*if (Input.GetKeyDown(KeyCode.T))
            DesapareceBarra();
        else if (Input.GetKeyDown(KeyCode.Y))
            ApareceBarra();*/

        //slider.gameObject.SetActive(enemigoCerca);

    }


    public void TomarDaño(float daño, bool envenenado)
    {
        vidaActual -= daño;
        if (envenenado)
        {
            if (estaEnvenenado)
            {
                StopCoroutine(coroutineActual);
                coroutineActual = StartCoroutine(CorEmpezarEnvenenamiento());
            }
            else
            {
                coroutineActual = StartCoroutine(CorEmpezarEnvenenamiento());
            }
        }
        Debug.Log("Vida Actual: " + vidaActual);
    }

    private void RegenerarVida()
    {
        if (!stopRegenerar)
            vidaActual = Mathf.Min(vidaMaxima, vidaActual + Time.deltaTime * velocidadRegeneracion);
    }

    public void ZonaPeligro(bool peligro)
    {

    }

    private void DesapareceBarra()
    {
        StartCoroutine(CorDesapareceBarra());
    }

    private void ApareceBarra()
    {
        backgroundBarra.color = new Color(backgroundBarra.color.r, backgroundBarra.color.g, backgroundBarra.color.b, 0f);
        frontBarra.color = new Color(frontBarra.color.r, frontBarra.color.g, frontBarra.color.b, 0f);
    }

    IEnumerator CorDesapareceBarra()
    {
        transparencia -= .2f;
        yield return new WaitForSeconds(2f);
        backgroundBarra.color = new Color(backgroundBarra.color.r, backgroundBarra.color.g, backgroundBarra.color.b, transparencia);
        frontBarra.color = new Color(frontBarra.color.r, frontBarra.color.g, frontBarra.color.b, transparencia);
    }

    IEnumerator CorEsperaregenerar()
    {
        vidaActual = 0.1f;
        estaStun = true;
        //GetComponent<Mov>().enabled = false;
        yield return new WaitForSeconds(tiempoStun);
        //GetComponent<Mov>().enabled = true;
        estaStun = false;
        stopRegenerar = false;
        GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }

    IEnumerator CorEmpezarEnvenenamiento()
    {
        estaEnvenenado = true;
        velocidadRegeneracion = veneno;
        frontBarra.color = Color.green;
        yield return new WaitForSeconds(duracionVeneno);
        velocidadRegeneracion = 3f;
        estaEnvenenado = false;
        frontBarra.color = Color.blue;
    }



    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Daño"))
            TomarDaño(20, true);
    }

    public bool EstaStun() => estaStun;
}

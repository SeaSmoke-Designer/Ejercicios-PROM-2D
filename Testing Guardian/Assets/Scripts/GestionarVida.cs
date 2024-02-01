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

    void Awake()
    {
        vidaActual = vidaMaxima;
        vidaText.text = vidaMaxima.ToString();
        //slider = GetComponent<Slider>();
        slider.maxValue = vidaMaxima;
        slider.value = vidaMaxima;
        transparencia = 1f;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        vidaText.text = vidaActual.ToString();
        if (vidaActual <= 0)
        {
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        if (vidaActual < 100)
            RegenerarVida();

        if (Input.GetKeyDown(KeyCode.T))
            DesapareceBarra();
        else if (Input.GetKeyDown(KeyCode.Y))
            ApareceBarra();
        slider.value = vidaActual;
        slider.gameObject.SetActive(enemigoCerca);

    }


    public void TomarDaño(float daño)
    {
        vidaActual -= daño;

        Debug.Log("Vida Actual: " + vidaActual);
    }

    private void RegenerarVida()
    {
        vidaActual = Mathf.Min(100f, vidaActual + Time.deltaTime * velocidadRegeneracion);
    }

    public void ZonaPeligro(bool peligro)
    {

    }

    private void DesapareceBarra()
    {
        while (transparencia <= 0)
        {
            StartCoroutine(CorDesapareceBarra());
        }
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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Daño"))
            TomarDaño(20);
    }
}

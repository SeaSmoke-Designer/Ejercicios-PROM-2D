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

    void Awake()
    {
        vidaActual = vidaMaxima;
        vidaText.text = vidaMaxima.ToString();
        //slider = GetComponent<Slider>();
        slider.maxValue = vidaMaxima;
        slider.value = vidaMaxima;
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
        slider.value = vidaActual;
        slider.gameObject.SetActive(enemigoCerca);
    }

    /*private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.CompareTag("Daño"))
            TomarDaño(15);
        Debug.Log("Vida Actual: " + vidaActual);
    }*/

    public void TomarDaño(float daño)
    {
        vidaActual -= daño;

        Debug.Log("Vida Actual: " + vidaActual);
    }

    private void RegenerarVida()
    {
        vidaActual += Time.deltaTime * 0.1f;

    }

    public void ZonaPeligro(bool peligro)
    {

    }

    IEnumerator SumarVida()
    {
        yield return new WaitForSeconds(10f);
        vidaActual += 1;
        yield break;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Daño"))
            TomarDaño(20);
    }
}

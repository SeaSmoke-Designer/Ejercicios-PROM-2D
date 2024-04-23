using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Texto_Progresivo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;

    [SerializeField] private string textoAEscribir;
    [SerializeField] private float tiempoEsperaCaracter;
    //[SerializeField] private Texture2D texto;
    void Start()
    {
        StartCoroutine(CorMaquinaEscribir());
    }

    IEnumerator CorMaquinaEscribir()
    {
        text.text = "";
        foreach (char character in textoAEscribir)
        {
            text.text += character;
            yield return new WaitForSeconds(tiempoEsperaCaracter);
        }

    }
}

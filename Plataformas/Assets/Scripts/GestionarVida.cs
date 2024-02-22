using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GestionarVida : MonoBehaviour
{
    [Range(1, 3)]
    [SerializeField] private float vidaMaxima;
    private float currentLife;

    [SerializeField] Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        currentLife = vidaMaxima;
        slider.value = vidaMaxima;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentLife -= damage;
        slider.value = currentLife;
    }
}

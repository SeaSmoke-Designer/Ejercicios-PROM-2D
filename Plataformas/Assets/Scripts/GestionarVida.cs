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

    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        if (currentLife > 0)
            currentLife -= damage;
        if (currentLife <= 0)
            Dead();
        slider.value = currentLife;
    }

    void Dead()
    {
        //currentLife = 0;
        //slider.value = currentLife;
        gm.PlayerDead();
    }

    public float GetCurrentLife() => currentLife;

    public void SetCurrentLife(float value)
    {
        currentLife = value;
        slider.value = currentLife;
    }


    public float GetVidaMaxima() => vidaMaxima;

    public void AplicarVidaMaxima()
    {
        currentLife = vidaMaxima;
        slider.value = vidaMaxima;
    }
}

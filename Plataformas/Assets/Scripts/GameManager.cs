using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GestionarVida gestionarVida;
    // Start is called before the first frame update
    void Start()
    {
        gestionarVida = GameObject.Find("Player").GetComponent<GestionarVida>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HitPlayer(float damage)
    {
        gestionarVida.TakeDamage(damage);
    }

}

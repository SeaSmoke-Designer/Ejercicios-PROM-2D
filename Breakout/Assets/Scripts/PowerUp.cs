using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private GameManager gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        try
        {
            if (gm.Win() && gameObject is not null)
                Destroy(gameObject);
        }
        catch (System.Exception ex)
        {
            Debug.Log("No se pudo");
            Debug.Log(ex.Message);
            //throw;
        }

        
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Paddle"))
        {
            if (gameObject.CompareTag("PowerUpVida"))
            {
                Debug.Log("Curar");
                gm.Curar();
                Destroy(gameObject);
            }
            else if (gameObject.CompareTag("PowerUpBola"))
            {
                Debug.Log("Bola");
                gm.AddBall();
                Destroy(gameObject);
            }
        }
        else if (other.gameObject.CompareTag("Dead"))
        {
            Destroy(gameObject);
        }
    }

    public void DestruirPowerUps()
    {
        Destroy(gameObject);
    }

}

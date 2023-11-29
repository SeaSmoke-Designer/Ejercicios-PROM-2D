using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ladrillos : MonoBehaviour
{
    [SerializeField]
    private List<Sprite> sprites;

    [SerializeField]
    private SpriteRenderer spriteRenderer;
    private float tipoLadrillo;
    private float vidaLadrillo;

    void Start()
    {
        tipoLadrillo = Random.Range(1, 6);
        vidaLadrillo = Random.Range(1, 5);
        ElegirTipo();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void ElegirTipo()
    {
        switch (tipoLadrillo)
        {
            case 1:
                LadrilloAzul();
                break;
            case 2:
                LadrilloVerde();
                break;
            case 3:
                LadrilloRojo();
                break;
            case 4:
                LadrilloVioleta();
                break;
            case 5:
                LadrilloAmarillo();
                break;
        }
    }
    void LadrilloAzul()
    {
        switch (vidaLadrillo)
        {
            case 1:
                spriteRenderer.sprite = sprites[0];
                break;
            case 2:
                spriteRenderer.sprite = sprites[1];
                break;
            case 3:
                spriteRenderer.sprite = sprites[2];
                break;
            case 4:
                spriteRenderer.sprite = sprites[3];
                break;

        }
    }
    void LadrilloVerde()
    {
        switch (vidaLadrillo)
        {
            case 1:
                spriteRenderer.sprite = sprites[4];
                break;
            case 2:
                spriteRenderer.sprite = sprites[5];
                break;
            case 3:
                spriteRenderer.sprite = sprites[6];
                break;
            case 4:
                spriteRenderer.sprite = sprites[7];
                break;

        }
    }
    void LadrilloRojo()
    {
        switch (vidaLadrillo)
        {
            case 1:
                spriteRenderer.sprite = sprites[8];
                break;
            case 2:
                spriteRenderer.sprite = sprites[9];
                break;
            case 3:
                spriteRenderer.sprite = sprites[10];
                break;
            case 4:
                spriteRenderer.sprite = sprites[11];
                break;

        }
    }
    void LadrilloVioleta()
    {
        switch (vidaLadrillo)
        {
            case 1:
                spriteRenderer.sprite = sprites[12];
                break;
            case 2:
                spriteRenderer.sprite = sprites[13];
                break;
            case 3:
                spriteRenderer.sprite = sprites[14];
                break;
            case 4:
                spriteRenderer.sprite = sprites[15];
                break;

        }
    }
    void LadrilloAmarillo()
    {
        switch (vidaLadrillo)
        {
            case 1:
                spriteRenderer.sprite = sprites[16];
                break;
            case 2:
                spriteRenderer.sprite = sprites[17];
                break;
            case 3:
                spriteRenderer.sprite = sprites[18];
                break;
            case 4:
                spriteRenderer.sprite = sprites[19];
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
            Destroy(gameObject);
        }
    }
}
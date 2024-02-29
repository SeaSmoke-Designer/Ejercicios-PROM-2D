using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GestionarVida gestionarVida;
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if(player != null) gestionarVida = player.GetComponent<GestionarVida>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HitPlayer(float damage)
    {
        player.Hit();
        gestionarVida.TakeDamage(damage);
    }

}

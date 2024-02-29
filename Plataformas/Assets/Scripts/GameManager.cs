using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GestionarVida gestionarVida;
    private Player player;
    //private Animator animator;
    private Death sceneTransitionDeath;
    [SerializeField] private GameObject hubVidas;
    // Start is called before the first frame update

    private void Awake()
    {
        hubVidas.SetActive(false);
    }
    void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player != null) gestionarVida = player.GetComponent<GestionarVida>();
        var sceneTransitionGO = GameObject.Find("SceneTransitionDeath");
        if (sceneTransitionGO != null) sceneTransitionDeath = sceneTransitionGO.GetComponent<Death>();
        //if (sceneTransitionGO != null) animator = sceneTransitionGO.GetComponent<Animator>();
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

    public void PlayerDead()
    {
        player.AnimaitionDead();
    }

    public void LanzarAnimacionDeath()
    {
        hubVidas.SetActive(false);
        sceneTransitionDeath.Muerte();
        //animator.SetTrigger("IsDead");
    }

    public void ActivarHubVidas()
    {
        hubVidas.SetActive(true);
    }

}

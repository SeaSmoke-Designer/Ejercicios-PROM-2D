using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private GestionarVida gestionarVida;
    private Player player;
    //private Animator animator;
    private Death sceneTransitionDeath;
    private SceneTransition sceneTransition;
    private UserDataManager userDataManager;
    [SerializeField] private GameObject hubVidas;
    [SerializeField] private AudioClip winSound;
    

    private void Awake()
    {
        //hubVidas.SetActive(false);
        userDataManager = GameObject.Find("UserDataManager").GetComponent<UserDataManager>();
        player = GameObject.Find("Player").GetComponent<Player>();
        if (player != null) gestionarVida = player.GetComponent<GestionarVida>();
        var sceneTransitionDeathGO = GameObject.Find("SceneTransitionDeath");
        if (sceneTransitionDeathGO != null) sceneTransitionDeath = sceneTransitionDeathGO.GetComponent<Death>();

        var sceneTransiotionGO = GameObject.Find("SceneTransition");
        if (sceneTransiotionGO != null) sceneTransition = sceneTransiotionGO.GetComponent<SceneTransition>();
    }
    void Start()
    {
        if (userDataManager.IsDead)
            AudioManager.Instance.StartAgain();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            userDataManager.Vidas = gestionarVida.GetVidaMaxima();
            gestionarVida.AplicarVidaMaxima();
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            AudioManager.Instance.StartAgain();
            if (userDataManager.IsDead)
            {
                userDataManager.Vidas = gestionarVida.GetVidaMaxima();
                gestionarVida.AplicarVidaMaxima();
            }
            else
                gestionarVida.SetCurrentLife(userDataManager.Vidas);
        }

        userDataManager.IsDead = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void HitPlayer(float damage)
    {
        player.Hit();
        gestionarVida.TakeDamage(damage);
        userDataManager.Vidas = gestionarVida.GetCurrentLife();
    }

    public void PlayerDead()
    {
        AudioManager.Instance.StopMusic();
        player.AnimationDesaparecer();
    }

    public void LanzarAnimacionDeath()
    {
        hubVidas.SetActive(false);
        sceneTransitionDeath.Muerte();
        userDataManager.IsDead = true;
        //animator.SetTrigger("IsDead");
    }

    public void ActivarHubVidas()
    {
        hubVidas.SetActive(true);
    }

    public void NextLevel()
    {
        AudioManager.Instance.StopMusic();
        AudioManager.Instance.PlayClip(winSound);
        player.AnimationDesaparecer();
        userDataManager.Vidas = gestionarVida.GetCurrentLife();
        hubVidas.SetActive(false);
        sceneTransition.CambiarEscena();
        StartCoroutine(CorEsperaCambioEscena());
    }

    IEnumerator CorEsperaCambioEscena()
    {
        yield return new WaitForSeconds(winSound.length);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public bool StayAlive()
    {
        return userDataManager.Vidas > 0;
    }

}

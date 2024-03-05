using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private Animator animator;
    private bool isDead;
    [SerializeField] private AudioClip darkSoulsSound;

    private bool animacionFinalizada;
    void Start()
    {
        animator = GetComponent<Animator>();
        isDead = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead && animacionFinalizada)
        {
            if (Input.anyKey)
                ChangeScene();
        }
           
    }

    public void Muerte()
    {
        //AudioManager.Instance.PlayClip(darkSoulsSound);
        animator.SetTrigger("IsDead");
        isDead = true;
    }



    void ChangeScene()
    {
        AudioManager.Instance.StopMusic();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FinishAnimation()
    {
        animacionFinalizada = true;
    }

    public void StartMusic() => AudioManager.Instance.PlayClip(darkSoulsSound);
}

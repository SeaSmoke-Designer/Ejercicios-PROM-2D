using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Death : MonoBehaviour
{
    private Animator animator;
    private bool isDead;

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
            if (Input.anyKey)
                ChangeScene();
    }

    public void Muerte()
    {
        animator.SetTrigger("IsDead");
        isDead = true;
    }



    void ChangeScene()
    {
        Debug.Log("Cambiar escena");
        //Save.Instance.die = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FinishAnimation()
    {
        //Revisar
        animacionFinalizada = true;
    }
}

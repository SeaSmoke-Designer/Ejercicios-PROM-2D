using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesplazarX : MonoBehaviour
{

    //[SerializeField] private GameObject principio;
    //[SerializeField] private GameObject fin;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float tiempoEspera;
    [SerializeField] private float velocity;
    private Rigidbody2D rb;
    private bool mover;
    private GameManager gm;
    private Animator animator;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        rb.velocity =  new Vector2(1f*velocity, rb.velocity.y);
        //sprite.flipX = true;
        mover = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mover)
        {
            if (transform.position.x <= minX)
            {
                StartCoroutine(CorMoverPositivo());
            }
            else if (transform.position.x >= maxX)
            {
                StartCoroutine(CorMoverNegativo());
            }
        }
        //gm.SetMoverSaw(mover);
        if(gameObject.CompareTag("Saw"))
            animator.SetBool("IsOn", mover);

    }

    IEnumerator CorMoverPositivo()
    {
        mover = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(tiempoEspera);
        sprite.flipX = true;
        rb.velocity = new Vector2(1f * velocity, rb.velocity.y);
        mover = true;
        
    }

    IEnumerator CorMoverNegativo()
    {
        mover = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(tiempoEspera);
        sprite.flipX = false;
        rb.velocity = new Vector2(-1f * velocity, rb.velocity.y);
        mover = true;
        
    }

    //public bool GetMover() => mover;


}

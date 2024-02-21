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
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private bool mover;
    private GameManager gm;
    private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sprite = GetComponent<SpriteRenderer>();
        rb.velocity =  new Vector2(1f*speed, rb.velocity.y);
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
    }

    IEnumerator CorMoverPositivo()
    {
        mover = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(tiempoEspera);
        rb.velocity = new Vector2(1f * speed, rb.velocity.y);
        sprite.flipX = true;
        mover = true;
        
    }

    IEnumerator CorMoverNegativo()
    {
        mover = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(tiempoEspera);
        rb.velocity = new Vector2(-1f * speed, rb.velocity.y);
        sprite.flipX = false;
        mover = true;
        
    }

    //public bool GetMover() => mover;


}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesplazarY : MonoBehaviour
{
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float tiempoEspera;
    [SerializeField] private float velocity;
    private Rigidbody2D rb;
    private bool mover;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //animator = GetComponent<Animator>();
        rb.velocity = new Vector2(1f * velocity, rb.velocity.y);
        mover = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mover)
        {
            if (transform.position.x <= minY)
            {
                StartCoroutine(CorMoverPositivo());
            }
            else if (transform.position.x >= maxY)
            {
                StartCoroutine(CorMoverNegativo());
            }
        }
        //gm.SetMoverSaw(mover);
        //animator.SetBool("IsOn", mover);

    }

    IEnumerator CorMoverPositivo()
    {
        mover = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(tiempoEspera);
        rb.velocity = new Vector2(1f * velocity, rb.velocity.y);
        mover = true;

    }

    IEnumerator CorMoverNegativo()
    {
        mover = false;
        rb.velocity = new Vector2(0, rb.velocity.y);
        yield return new WaitForSeconds(tiempoEspera);
        rb.velocity = new Vector2(-1f * velocity, rb.velocity.y);
        mover = true;

    }
}

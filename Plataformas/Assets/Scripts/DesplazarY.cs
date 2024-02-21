using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesplazarY : MonoBehaviour
{
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float tiempoEspera;
    [SerializeField] private float speed;
    private Rigidbody2D rb;
    private bool mover;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        rb.velocity = new Vector2(0, 1f * speed);
        mover = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (mover)
        {
            if (transform.position.y <= minY)
            {
                StartCoroutine(CorMoverPositivo());
            }
            else if (transform.position.y >= maxY)
            {
                StartCoroutine(CorMoverNegativo());
            }
        }
    }

    IEnumerator CorMoverPositivo()
    {
        mover = false;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(tiempoEspera);
        rb.velocity = new Vector2(rb.velocity.x, 1f * speed);
        mover = true;

    }

    IEnumerator CorMoverNegativo()
    {
        mover = false;
        rb.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(tiempoEspera);
        rb.velocity = new Vector2(rb.velocity.x, -1f * speed);
        mover = true;

    }
}

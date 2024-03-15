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
    private bool isMoving;
    private GameManager gm;
    private SpriteRenderer sprite;
    float mov = 0f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        sprite = GetComponent<SpriteRenderer>();
        isMoving = true;
        mov = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > minX && transform.position.x < maxX)
        {
            isMoving = true;
        }
        else if (transform.position.x <= minX && isMoving)
        {
            StartCoroutine(CorMoverPositivo());
        }
        else if (transform.position.x >= maxX && isMoving)
        {
            StartCoroutine(CorMoverNegativo());
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(mov * speed, rb.velocity.y);
    }

    IEnumerator CorMoverPositivo()
    {
        mov = 0f;
        isMoving = false;
        yield return new WaitForSeconds(tiempoEspera);
        mov = 1f;
        sprite.flipX = true;
    }

    IEnumerator CorMoverNegativo()
    {
        mov = 0f;
        isMoving = false;
        yield return new WaitForSeconds(tiempoEspera);
        mov = -1f;
        sprite.flipX = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool isPlayer1;
    public float speed;
    public Rigidbody2D rb;
    private float movement;
    public Vector3 startPosition { get; private set; }
    private Vector3 startScale;

    protected void Start()
    {
        startPosition = transform.position;
        startScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlayer1)
            movement = Input.GetAxisRaw("Vertical");
        else
            movement = Input.GetAxisRaw("Vertical2");

        rb.velocity = new Vector2(rb.velocity.x, movement * speed);
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
    }

    public void IncreasePaddle()
    {
        transform.localScale += new Vector3(0, 0.5f, 0);
    }
    //Arreglar esto
    public void ShortenPaddle()
    {
        Vector3 dist = transform.localScale - new Vector3(0, 0.5f, 0);
        if (dist.y >= startScale.y)
            transform.localScale -= new Vector3(0, 0.5f, 0);
    }

    public void ResetSize()
    {
        transform.localScale = startScale;
    }

    /* void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Ball"))
        {
            if (gameObject.CompareTag("Player1"))
                collision2D.gameObject.GetComponent<Ball>().AumentarVelocidadBall(true);
            else if (gameObject.CompareTag("Player2"))
                collision2D.gameObject.GetComponent<Ball>().AumentarVelocidadBall(false);

        }
    } */
}

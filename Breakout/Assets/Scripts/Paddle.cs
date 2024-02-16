using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float speed;

    private float movement;
    private Vector3 startPosition;

    private GameObject ball;
    private GameManager gm;

    void Start()
    {
        startPosition = transform.position;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        //ball = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        PaddleMove();
    }

    void PaddleMove()
    {
        movement = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
        //ball.GetComponent<Ball>().BallMove(movement * speed);
    }

    /*public void ResetPaddle()
    {
        transform.position = startPosition;
    }*/


    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("PowerUpVida"))
        {
            Debug.Log("Curar");
            gm.Curar();
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.CompareTag("PowerUpBola"))
        {
            Debug.Log("Bola");
            Destroy(collision.gameObject);
        }

    }*/

}

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

    void Start()
    {
        startPosition = transform.position;
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
}

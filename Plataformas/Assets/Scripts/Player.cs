using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField]
    private float speed;
    private float movementH;
    private bool isJump;
    [SerializeField]
    private float jumpSpeed;
    [SerializeField]
    private float doubleJumpSpeed;
    private bool isDoubleJump;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    [SerializeField]
    LayerMask mapLayer;



    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
    }


    void Update()
    {
        movementH = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            isJump = true;
        animator.SetBool("IsRunning", movementH != 0f);
    }

    void FixedUpdate()
    {
        if (movementH == 1)
            GetComponent<SpriteRenderer>().flipX = false;
        else if (movementH == -1)
            GetComponent<SpriteRenderer>().flipX = true;

        //float speed2 = speed * movementH * Time.fixedDeltaTime;

        rb.velocity = new Vector2(movementH * speed, rb.velocity.y);
        if (isJump)
            Jump();

    }

    private void Jump()
    {
        if (isGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            isDoubleJump = true;
        }
        else
        {
            if (isDoubleJump)
            {
                rb.velocity = new Vector2(rb.velocity.x, doubleJumpSpeed);
                isDoubleJump = false;
            }
        }

        isJump = false;
    }

    bool isGrounded()
    {
        var boxCastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, Vector2.down, 0.1f, mapLayer);
        return boxCastHit.collider != null;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isDoubleJump = true;
        }
    }
}

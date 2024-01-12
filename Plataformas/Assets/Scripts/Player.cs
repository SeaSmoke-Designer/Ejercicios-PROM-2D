using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float doubleJumpSpeed;
    private float movementH;
    private bool isJump;
    [SerializeField] private float jumpSpeed;
    private bool isDoubleJump;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    [SerializeField] LayerMask mapLayer;
    [SerializeField] private float castDistanceX;
    [SerializeField] private float castDistanceY;
    private bool facingRight;


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
        animator.SetBool("IsGrounded", IsGrounded());
        animator.SetBool("OnWall", isNextToTheWall());
    }

    void FixedUpdate()
    {
        if (movementH == 1)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            facingRight = true;
        }
        else if (movementH == -1)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            facingRight = false;
        }

        rb.velocity = new Vector2(movementH * speed, rb.velocity.y);
        if (isJump)
            Jump();



    }

    private void Jump()
    {
        if (IsGrounded())
        {
            animator.SetTrigger("IsJump");
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            isDoubleJump = true;
        }
        else if (isDoubleJump || isNextToTheWall())
            DoubleJump();


        isJump = false;
    }
    void DoubleJump()
    {
        animator.SetTrigger("DoubleJump");
        rb.velocity = new Vector2(rb.velocity.x, doubleJumpSpeed);
        isDoubleJump = false;
    }

    bool IsGrounded()
    {
        Vector2 box = new Vector2(boxCollider2D.bounds.size.x - castDistanceX, boxCollider2D.bounds.size.y - castDistanceY);
        var boxCastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, box, 0, Vector2.down, 0.1f, mapLayer);
        return boxCastHit.collider != null;
    }


    bool isNextToTheWall()
    {
        Vector2 directionToTest = facingRight ? Vector2.right : Vector2.left;
        var boxCastHit = Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0, directionToTest, 0.1f, mapLayer);
        return boxCastHit.collider != null;
    }

    /*void OnDrawGizmos()
   {
       if (boxCollider2D == null) return;
       Gizmos.color = Color.red;

       var cubeOrigin = new Vector2(boxCollider2D.bounds.center.x, boxCollider2D.bounds.center.y - castDistanceY);
       var cubeSize = new Vector2(boxCollider2D.bounds.size.x - castDistanceX, boxCollider2D.bounds.size.y);
       Gizmos.DrawWireCube(cubeOrigin, cubeSize);

       /*Vector2 rayCastOrigin = new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y);
       Gizmos.DrawLine(rayCastOrigin, rayCastOrigin - new Vector2(0, castDistance));
   }*/
}


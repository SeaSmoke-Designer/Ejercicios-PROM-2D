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
    private bool saltandoDePared;
    private bool deslizando;
    [SerializeField] private float saltoPared;
    [SerializeField] private float tiempoSaltoPared;
    private SpriteRenderer sprite;
    [SerializeField] private float tiempoHit;
    [SerializeField] private float hitEmpujeX;
    [SerializeField] private float hitEmpujeY;
    private bool hit;
    private GameManager gm;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    void Update()
    {
        movementH = Input.GetAxisRaw("Horizontal");
        if (Input.GetKeyDown(KeyCode.Space))
            isJump = true;

        if (!IsGrounded() && isNextToTheWall() && movementH != 0)
            deslizando = true;
        else
            deslizando = false;
        animator.SetBool("IsRunning", movementH != 0f);
        animator.SetBool("IsGrounded", IsGrounded());
        animator.SetBool("OnWall", isNextToTheWall());
    }

    void FixedUpdate()
    {
        FlipPlayer();
        if (!saltandoDePared && !hit)
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
        else if (isDoubleJump)
            DoubleJump();
        else if (deslizando)
            SaltoPared();

        isJump = false;
    }

    void SaltoPared() //Saltar desde la pared
    {
        isDoubleJump = false;
        animator.SetTrigger("DoubleJump");
        rb.velocity = new Vector2(saltoPared * -movementH, doubleJumpSpeed);
        deslizando = false;
        StartCoroutine(CambioSaltoPared());
    }

    IEnumerator CambioSaltoPared()
    {
        saltandoDePared = true;
        yield return new WaitForSeconds(tiempoSaltoPared);
        saltandoDePared = false;
    }
    void DoubleJump()
    {
        if (!deslizando)
        {
            animator.SetTrigger("DoubleJump");
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpSpeed);
            isDoubleJump = false;
        }
        else
        {
            SaltoPared();
        }

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

    void FlipPlayer()
    {
        if (movementH == 1)
        {
            sprite.flipX = false;
            facingRight = true;
        }
        else if (movementH == -1)
        {
            sprite.flipX = true;
            facingRight = false;
        }
    }

    public void Hit()
    {

        StartCoroutine(CorStopMover());
        //Debug.Log(rb.velocity.x);
    }

    IEnumerator CorStopMover()
    {
        hit = true;
        animator.SetTrigger("Hit");
        rb.AddForce(new Vector2(hitEmpujeX * -movementH, hitEmpujeY), ForceMode2D.Impulse);
        yield return new WaitForSeconds(tiempoHit);
        hit = false;
    }

    public void AnimaitionDead()
    {
        animator.SetTrigger("IsDead");

    }
    public void DesactivarPlayer()
    {
        Debug.Log("Lanzo animacion");
        gameObject.SetActive(false);
        gm.LanzarAnimacionDeath();
        
    }



    /*void OnDrawGizmos()
    {
        if (boxCollider2D == null) return;
        Gizmos.color = Color.red;

        var cubeOrigin = new Vector2(boxCollider2D.bounds.center.x, boxCollider2D.bounds.center.y - castDistanceY);
        var cubeSize = new Vector2(boxCollider2D.bounds.size.x - castDistanceX, boxCollider2D.bounds.size.y);
        Gizmos.DrawWireCube(cubeOrigin, cubeSize);

        //Vector2 rayCastOrigin = new Vector2(boxCollider.bounds.center.x, boxCollider.bounds.min.y);
        //Gizmos.DrawLine(rayCastOrigin, rayCastOrigin - new Vector2(0, castDistance));
    }*/
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Parametros velocidad Player")]
    [SerializeField] private float speed;
    [SerializeField] private float doubleJumpSpeed;
    [SerializeField] private float jumpSpeed;
    private Rigidbody2D rb;
    private float movementH;
    private bool isJump;
    private bool isDoubleJump;
    private BoxCollider2D boxCollider2D;
    private Animator animator;
    [Header("Map Layer")]
    [SerializeField] LayerMask mapLayer;
    [Header("BoxCast")]
    [SerializeField] private float castDistanceX;
    [SerializeField] private float castDistanceY;
    private bool facingRight;
    private bool saltandoDePared;
    private bool deslizando;
    [Header("Parametros salto pared")]
    [SerializeField] private float saltoPared;
    [SerializeField] private float tiempoSaltoPared;
    private SpriteRenderer sprite;
    [Header("Parametros hit")]
    [SerializeField] private float tiempoHit;
    [SerializeField] private float hitEmpujeX;
    [SerializeField] private float hitEmpujeY;
    private bool hit;
    private GameManager gm;
    [Header("Sonidos")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip doubleJumpSound;
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip desapareceSound;
    [SerializeField] private AudioClip saltoParedSound;


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
        Animaciones();
    }

    void FixedUpdate()
    {
        FlipPlayer();
        if (!saltandoDePared && !hit)
            rb.velocity = new Vector2(movementH * speed, rb.velocity.y);

        if (isJump)
            Jump();
    }

    void Animaciones()
    {
        animator.SetBool("IsRunning", movementH != 0f);
        animator.SetBool("IsGrounded", IsGrounded());
        animator.SetBool("OnWall", isNextToTheWall());
    }

    private void Jump()
    {
        if (IsGrounded())
        {
            AudioManager.Instance.PlayClip(jumpSound);
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
        AudioManager.Instance.PlayClip(saltoParedSound);
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
            AudioManager.Instance.PlayClip(doubleJumpSound);
            animator.SetTrigger("DoubleJump");
            rb.velocity = new Vector2(rb.velocity.x, doubleJumpSpeed);
            isDoubleJump = false;
        }
        else
            SaltoPared();


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
    }

    IEnumerator CorStopMover()
    {
        AudioManager.Instance.PlayClip(hitSound);
        hit = true;
        animator.SetTrigger("Hit");
        rb.AddForce(new Vector2(hitEmpujeX * -movementH, hitEmpujeY), ForceMode2D.Impulse);
        yield return new WaitForSeconds(tiempoHit);
        hit = false;
    }

    public void AnimacionDesaparecer()
    {
        AudioManager.Instance.PlayClip(desapareceSound);
        animator.SetTrigger("IsDead");
    }
    public void DesactivarPlayer()
    {
        //Debug.Log("Lanzo animacion");
        gameObject.SetActive(false);
        if (!gm.StayAlive())
        {
            gm.LanzarAnimacionDeath();
        }


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


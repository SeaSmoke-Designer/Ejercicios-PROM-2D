using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 6;
    Animator ac;
    SpriteRenderer sr;
    public float movement;
    public bool jump = false;
    bool facingRight;
    BoxCollider2D boxCollider;
    public LayerMask mapLayer;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        rb.gravityScale = 5;
        //ac = GetComponent<Animator>();
        //sr = GetComponent<SpriteRenderer>();
    }

    bool isGrounded()
    {
        var boxCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, mapLayer);
        return boxCastHit.collider != null;
    }

    bool isNextToheWall()
    {
        Vector2 directionToTest = facingRight ? Vector2.right : Vector2.left;
        var boxCastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, directionToTest, 0.1f, mapLayer);
        return boxCastHit.collider != null;
    }

    void FixedUpdate()
    {
        if (jump == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, (speed * 3));
            //ac.SetTrigger("Jump");
            jump = false;
        }
        rb.velocity = new Vector2(movement * speed, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        movement = Input.GetAxisRaw("Horizontal");
        if (movement == 1)
        {
            facingRight = true;
            //sr.flipX = false;
        }
        if (movement == -1)
        {
            facingRight = false;
            //sr.flipX = true;
        }
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            jump = true;
        }
        //ac.SetBool("Mov", movement != 0);
        //ac.SetBool("Grounded", isGrounded());
    }

}

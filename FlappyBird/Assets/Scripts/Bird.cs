using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField]
    float jumpSpeed;

    [SerializeField]
    Rigidbody2D rb;

    private bool isJump;

    private float movement;

    private GameObject manager;

    [SerializeField]
    AudioClip jumpAudio;

    [SerializeField]
    AudioClip scoreAudio;

    [SerializeField]
    AudioClip explosionAudio;

    // Start is called before the first frame update
    void Start()
    {
        manager = GameObject.Find("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJump = true;
            //movement = Input.GetAxisRaw("Vertical");
            //rb.velocity = new Vector2(rb.velocity.x,movement*jumpSpeed);
        }
    }

    void FixedUpdate()
    {
        if (isJump)
        {
            AudioManager.Instance.PlayClip(jumpAudio);
            movement = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x, movement * jumpSpeed * Time.fixedDeltaTime);
            isJump = false;
        }
    }


    void OnTriggerEnter2D(Collider2D collider2D)
    {
        if (collider2D.gameObject.CompareTag("Dead"))
        {
            AudioManager.Instance.PlayClip(explosionAudio);
            manager.GetComponent<GameManager>().KillBird();
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Point"))
        {
            manager.GetComponent<GameManager>().Point();
            AudioManager.Instance.PlayClip(scoreAudio);
        }

    }
}

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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            isJump = true;
            //movement = Input.GetAxisRaw("Vertical");
            //rb.velocity = new Vector2(rb.velocity.x,movement*jumpSpeed);
        }
    }

    void FixedUpdate()
    {
        if(isJump){
            movement = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x,movement*jumpSpeed*Time.fixedDeltaTime);
            isJump = false;
        }
    }


    void OnTriggerEnter2D(Collider2D collider2D){
        if(collider2D.gameObject.tag.Equals("Dead")){
            Debug.Log("Muerte");
            GameObject.Find("GameManager").GetComponent<GameManager>().KillBird();
        }
    }
}

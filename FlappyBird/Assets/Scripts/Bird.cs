using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    public float jumpSpeed;
    public Rigidbody2D rb;

    private float movement;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)){
            movement = Input.GetAxisRaw("Vertical");
            rb.velocity = new Vector2(rb.velocity.x,movement*jumpSpeed);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesplazarX : MonoBehaviour
{

    //[SerializeField] private GameObject principio;
    //[SerializeField] private GameObject fin;
    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float tiempoEspera;
    [SerializeField] private float velocity;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity =  new Vector2(1f*velocity, rb.velocity.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x <= minX)
        {
            rb.velocity = new Vector2(1f * velocity, rb.velocity.y);
        }else if(transform.position.x >= maxX)
        {
            rb.velocity = new Vector2(-1f * velocity, rb.velocity.y);
        }
    }


}

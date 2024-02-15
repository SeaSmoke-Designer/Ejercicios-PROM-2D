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

        if (gameObject.transform.position.x > minX)
        {

        }


    }

    // Update is called once per frame
    void Update()
    {

    }


}

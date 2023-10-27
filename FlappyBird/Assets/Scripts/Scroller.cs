using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    private float posicionInicio = 11.38f;
    private float saleCamara = -29.36f;

    private float tp = 28.45f;

    [SerializeField]
    GameObject background1;
    
    [SerializeField]
    GameObject background2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        background1.transform.Translate(new Vector3(-0.5f*Time.deltaTime,0,0));
        background2.transform.Translate(new Vector3(-0.5f*Time.deltaTime,0,0));
        if(background1.transform.position.x < saleCamara){
            background1.transform.position = new Vector3(tp,transform.position.y,0);
        }
    }

    void FixedUpdate()
    {
        
    }
}

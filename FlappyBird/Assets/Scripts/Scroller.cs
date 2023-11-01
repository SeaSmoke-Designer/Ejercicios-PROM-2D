using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    //private float posicionInicio = 11.38f;
    //private float saleCamara = -29.36f;

    //private float tp = 28.45f;

    [SerializeField]
    float parallaxSpeed;

    [SerializeField]
    float begin;

    [SerializeField]
    float end;

    //private float resetPosition = -29;
    //private float initialOffset = 29.1619f - 28.44f;

    //[SerializeField]
    //GameObject background1;
    
    //[SerializeField]
    //GameObject background2;

    // Start is called before the first frame update
    void Start()
    {
        //background1.transform.position = new Vector3(11.36f, background1.transform.position.y, background1.transform.position.z);
        //background2.transform.position = new Vector3(11.36f + initialOffset, background2.transform.position.y, background2.transform.position.z);
    }
    //11.36
    //40.46 / 40.27
    /* --------------------------------------------------------------------------------------------------*/
    //0.26
    //44.7
    //

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(parallaxSpeed * Time.fixedDeltaTime, 0, 0);
        if(transform.position.x <= end)
        {
            Vector2 start = new Vector2(begin, transform.position.y);
            transform.position = start;
        }


        /*float backgroundSpeed = parallaxSpeed * Time.fixedDeltaTime;
        background1.transform.Translate(new Vector3(backgroundSpeed, 0, 0));
        background2.transform.Translate(new Vector3(backgroundSpeed, 0, 0));
        if (background1.transform.position.x < -29.31f)
        {
            background1.transform.position = new Vector3(28.44f, transform.position.y, 0);
        }
        if (background2.transform.position.x < -29.31f) //-28.72
        {
            background2.transform.position = new Vector3(28.59f, transform.position.y, 0); //29.1619
        }

        float backgroundSpeed = parallaxSpeed * Time.fixedDeltaTime;
        background1.transform.Translate(new Vector3(backgroundSpeed, 0, 0));
        background2.transform.Translate(new Vector3(backgroundSpeed, 0, 0));
        if (background1.transform.position.x < resetPosition)
        {
            background1.transform.position = ResetBackgroundPosition(background1, background2);
        }
        else if (background2.transform.position.x < resetPosition)
        {
            background2.transform.position = ResetBackgroundPosition(background2, background1);
        }*/
    }

    /*Vector3 ResetBackgroundPosition(GameObject bgToReset, GameObject otherBg)
    {
        return new Vector3(otherBg.transform.position.x + resetPosition * 2, bgToReset.transform.position.y, bgToReset.transform.position.z);
        //bgToReset.transform.position = newPosition;
    }*/

}

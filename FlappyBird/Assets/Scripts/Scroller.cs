using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    //private float posicionInicio = 11.38f;
    //private float saleCamara = -29.36f;

    //private float tp = 28.45f;

    //[SerializeField]
    private float parallaxSpeedBackground;
    private float parallaxSpeedGround;

    //[SerializeField]
    private float beginBackground1;
    private float beginBackground2;

    //[SerializeField]
    float end;
    private float diferencia;

    //private float resetPosition = -29;
    //private float initialOffset = 29.1619f - 28.44f;

    [SerializeField]
    GameObject background1;
    
    [SerializeField]
    GameObject background2;

    [SerializeField]
    GameObject ground1;

    [SerializeField]
    GameObject ground2;

    // Start is called before the first frame update
    void Start()
    {
        //background1.transform.position = new Vector3(11.36f, background1.transform.position.y, background1.transform.position.z);
        //background2.transform.position = new Vector3(11.36f + initialOffset, background2.transform.position.y, background2.transform.position.z);
    }

    void Awake()
    {
        beginBackground1 = background1.transform.position.x;
        beginBackground2 = background2.transform.position.x;
        diferencia = beginBackground2 - beginBackground1;
        parallaxSpeedBackground = -1.2f;
        end = -29.28f;
    }
    //11.36
    //40.46 / 40.27
    /* --------------------------------------------------------------------------------------------------*/
    //0.26
    //44.7
    //


    //begin background1: 2.88163
    //begin background2: 31.8
    // sale de camara: -37.28

    // Update is called once per frame
    private float resetPosition = 21.03f;
    void Update()
    {
        float backgroundSpeed =  parallaxSpeedBackground * Time.deltaTime;
        background1.transform.Translate(Vector2.right.x*backgroundSpeed, 0, 0);
        background2.transform.Translate(Vector2.right.x*backgroundSpeed,0,0);
        if (background1.transform.position.x <= end)
        {
            
            background1.transform.position  = new Vector2(beginBackground1+diferencia, background1.transform.position.y);
            //transform.position = start;
        }
        if(background2.transform.position.x <= end){
            background2.transform.position = new Vector2(beginBackground1+diferencia, background2.transform.position.y);
        }

    }

    void FixedUpdate()
    {
        


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

    public void PipesMove(GameObject pipe)
    {
        //pipe.transform.Translate(parallaxSpeed * Time.deltaTime, 0, 0);
    }
}

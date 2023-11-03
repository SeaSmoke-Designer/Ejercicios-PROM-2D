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
    //private float beginBackground1;
    //private float beginBackground2;

    //[SerializeField]
    private float endBackground;

    private float endGround;
    private float differenceBackground;
    private float differenceGround;

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
        
    }

    void Awake()
    {
        BackgroundsDifference();
        GroundsDiference();
    }

    // Update is called once per frame
    
    void Update()
    {
        BackgroundsMove();
        GroundsMove();
    }

    void FixedUpdate()
    {
        
    }


    public void PipesMove(GameObject pipe)
    {
        pipe.transform.Translate(parallaxSpeedGround * Time.deltaTime, 0, 0);
    }

    void BackgroundsMove(){
        float backgroundSpeed =  parallaxSpeedBackground * Time.deltaTime;
        background1.transform.Translate(Vector2.right.x*backgroundSpeed, 0, 0);
        background2.transform.Translate(Vector2.right.x*backgroundSpeed,0,0);
        if (background1.transform.position.x <= endBackground){
            background1.transform.position  = new Vector2(background2.transform.position.x+differenceBackground, background1.transform.position.y);
        }
        if(background2.transform.position.x <= endBackground){
            background2.transform.position = new Vector2(background1.transform.position.x+differenceBackground, background2.transform.position.y);
        }
    }

    void GroundsMove(){
        float groundSpeed =  parallaxSpeedGround * Time.deltaTime;
        ground1.transform.Translate(Vector2.right.x*groundSpeed, 0, 0);
        ground2.transform.Translate(Vector2.right.x*groundSpeed,0,0);
        if (ground1.transform.position.x <= endGround){
            ground1.transform.position  = new Vector2(ground2.transform.position.x+differenceGround, ground1.transform.position.y);
        }
        if(ground2.transform.position.x <= endGround){
            ground2.transform.position = new Vector2(ground1.transform.position.x+differenceGround, ground2.transform.position.y);
        }
    }

    void BackgroundsDifference(){
        differenceBackground = background2.transform.position.x - background1.transform.position.x;
        parallaxSpeedBackground = -0.5f;
        endBackground= -29.28f;
    }
    void GroundsDiference(){
        differenceGround = ground2.transform.position.x - ground1.transform.position.x;
        parallaxSpeedGround = -1.2f;
        endGround = -19.9f;
    }
}

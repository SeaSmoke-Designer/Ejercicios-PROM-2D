using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    private float parallaxSpeedBackground;
    private float parallaxSpeedGround;
    private float endBackground;

    private float endGround;
    private float differenceBackground;
    private float differenceGround;

    [SerializeField]
    GameObject background1;

    [SerializeField]
    GameObject background2;

    [SerializeField]
    GameObject ground1;

    [SerializeField]
    GameObject ground2;

    private List<GameObject> pipesPair;
    private bool movePipes = false;

    // Start is called before the first frame update
    void Start()
    {
        pipesPair = new List<GameObject>();
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
        if (movePipes)
            PipeMove();
    }

    void FixedUpdate()
    {

    }


    public void PipesMove(List<GameObject> pipes)
    {
        pipesPair = pipes;
        movePipes = true;
    }

    public void PipeMove()
    {
        float speed = parallaxSpeedGround * Time.deltaTime;
        foreach (GameObject pipe in pipesPair)
        {
            pipe.transform.Translate(Vector2.right.x * speed, 0, 0);
        }
    }

    void BackgroundsMove()
    {
        float backgroundSpeed = parallaxSpeedBackground * Time.deltaTime;
        background1.transform.Translate(Vector2.right.x * backgroundSpeed, 0, 0);
        background2.transform.Translate(Vector2.right.x * backgroundSpeed, 0, 0);
        if (background1.transform.position.x <= endBackground)
        {
            background1.transform.position = new Vector2(background2.transform.position.x + differenceBackground, background1.transform.position.y);
        }
        if (background2.transform.position.x <= endBackground)
        {
            background2.transform.position = new Vector2(background1.transform.position.x + differenceBackground, background2.transform.position.y);
        }
    }

    void GroundsMove()
    {
        float groundSpeed = parallaxSpeedGround * Time.deltaTime;
        ground1.transform.Translate(Vector2.right.x * groundSpeed, 0, 0);
        ground2.transform.Translate(Vector2.right.x * groundSpeed, 0, 0);
        if (ground1.transform.position.x <= endGround)
        {
            ground1.transform.position = new Vector2(ground2.transform.position.x + differenceGround, ground1.transform.position.y);
        }
        if (ground2.transform.position.x <= endGround)
        {
            ground2.transform.position = new Vector2(ground1.transform.position.x + differenceGround, ground2.transform.position.y);
        }
    }

    void BackgroundsDifference()
    {
        differenceBackground = background2.transform.position.x - background1.transform.position.x;
        parallaxSpeedBackground = -0.5f;
        endBackground = -29.28f;
    }
    void GroundsDiference()
    {
        differenceGround = ground2.transform.position.x - ground1.transform.position.x;
        parallaxSpeedGround = -1.2f;
        endGround = -19.9f;
    }

    public void DestroyPipes()
    {
        foreach (GameObject pipe in pipesPair)
        {
            GameObject.Destroy(pipe);
        }
        pipesPair.Clear();
        movePipes = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speedBall;
    [SerializeField]
    private Rigidbody2D rb;
    private Vector3 startPosition;

    bool isLaunch;

    private GameObject paddle;
    void Start()
    {
        startPosition = transform.position;
        paddle = GameObject.Find("Paddle");
        isLaunch = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLaunch)
            transform.position = new Vector3(paddle.GetComponent<Paddle>().transform.position.x, startPosition.y, startPosition.z);

    }

    public void Launch()
    {
        isLaunch = true;
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = 1;
        rb.velocity = new Vector2(x * speedBall, y * speedBall);
    }


}

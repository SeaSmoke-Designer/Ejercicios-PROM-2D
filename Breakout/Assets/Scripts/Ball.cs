using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private float speedBall;
    [SerializeField]
    private Rigidbody2D rb;
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? 1f : 1;
        float y = Random.Range(-4, -3) == -4 ? 1f : 1;
        rb.velocity = new Vector2(x * speedBall, y * speedBall);
    }
}

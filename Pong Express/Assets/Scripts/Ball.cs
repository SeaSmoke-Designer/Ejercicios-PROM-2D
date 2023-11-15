using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speedBall;
    private float startSpeedBall;
    public Rigidbody2D rb;
    private Vector3 startPosition;

    //private bool isPlaying = false;

    public AudioSource audioSource;
    public AudioClip audioClipPaddle;
    public AudioClip audioClipWall;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
        startSpeedBall = speedBall;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Launch()
    {
        float x = Random.Range(0, 2) == 0 ? -1 : 1;
        float y = Random.Range(0, 2) == 0 ? -1 : 1;
        rb.velocity = new Vector2(x * speedBall, y * speedBall);
    }

    public void Reset()
    {
        rb.velocity = Vector2.zero;
        transform.position = startPosition;
        speedBall = startSpeedBall;
    }
    public void AumentarVelocidadBall(bool isPlayer1)
    {
        float x, y = 0;
        if (isPlayer1)
            x = 1;
        else
            x = -1;

        if (rb.velocity.y > 0)
            y = 1;
        else if (rb.velocity.y < 0)
            y = -1;

        speedBall += 0.3f;
        rb.velocity = new Vector2(x * speedBall, y * speedBall);
    }

    void OnCollisionEnter2D(Collision2D collision2D)
    {
        if (collision2D.gameObject.tag.Equals("Player1") || collision2D.gameObject.tag.Equals("Player2"))
        {
            audioSource.PlayOneShot(audioClipPaddle);
        }
        else if (collision2D.gameObject.tag.Equals("Wall"))
            audioSource.PlayOneShot(audioClipWall);
    }

}

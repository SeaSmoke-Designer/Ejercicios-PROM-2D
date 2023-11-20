using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool isPlayer1Wall;
    public AudioSource audioSource;
    public AudioClip audioClip;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            audioSource.PlayOneShot(audioClip);
            if (!isPlayer1Wall)
                GameObject.Find("GameManager").GetComponent<GameManager>().Player1Scores();
            else
                GameObject.Find("GameManager").GetComponent<GameManager>().Player2Scores();
        }
    }
}

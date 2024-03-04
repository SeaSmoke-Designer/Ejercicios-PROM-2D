using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0.5f,1)]
    [SerializeField] private float damage;
    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        if (gameObject.CompareTag("Death")) damage = 3f;
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log(damage);
            gm.HitPlayer(damage);
        }
    }
}

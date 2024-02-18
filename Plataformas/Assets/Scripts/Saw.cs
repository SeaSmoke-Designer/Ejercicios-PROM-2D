using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Saw : MonoBehaviour
{
    // Start is called before the first frame update
    private Animator animator;
    private GameManager gm;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsOn", gm.GetMoverSaw()); 

    }
}

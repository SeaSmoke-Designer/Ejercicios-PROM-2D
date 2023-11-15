using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState : IState
{
    private AutoPaddle autoPaddle;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public ActiveState(AutoPaddle paddle)
    {
        this.autoPaddle = paddle;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InactiveState : IState
{
    private AutoPaddle autoPaddle;

    Rigidbody2D ball;

    public InactiveState(AutoPaddle paddle)
    {
        this.autoPaddle = paddle;
        this.ball = GameObject.Find("Ball").GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (ball.transform.position.x > 0)
        {
            autoPaddle.autoPaddleFMS.TransitionTo(autoPaddle.autoPaddleFMS.activeState);
        }
    }


}

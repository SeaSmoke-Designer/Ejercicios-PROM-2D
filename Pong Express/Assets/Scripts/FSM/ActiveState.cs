using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveState : IState
{
    private AutoPaddle autoPaddle;

    Rigidbody2D ball;

    //GameObject paddle2;
    public ActiveState(AutoPaddle paddle)
    {
        this.autoPaddle = paddle;
        this.ball = GameObject.Find("Ball").GetComponent<Rigidbody2D>();
        //this.paddle2 = GameObject.Find("Paddle");
    }

    // Update is called once per frame
    public void Update()
    {
        if (ball.velocity.x < 0)
        {
            autoPaddle.autoPaddleFMS.TransitionTo(autoPaddle.autoPaddleFMS.inactiveState);
        }
        float diferencia = ball.transform.position.y - autoPaddle.transform.position.y;

        if (diferencia > 0.1f)
        {
            autoPaddle.rb.velocity = new Vector2(0, autoPaddle.speed * 1);
        }
        else if (diferencia < 0)
        {
            autoPaddle.rb.velocity = new Vector2(0, autoPaddle.speed * -1);
        }
    }


}

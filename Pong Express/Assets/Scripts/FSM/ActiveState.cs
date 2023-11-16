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
    void Update()
    {
        if (ball.transform.position.x < 0)
        {
            autoPaddle.autoPaddleFMS.TransitionTo(autoPaddle.autoPaddleFMS.inactiveState);
        }
        float diferecnia = ball.transform.position.y - autoPaddle.transform.position.y;
        if (diferecnia > 0)
        {
            autoPaddle.rb.velocity = new Vector2(autoPaddle.rb.velocity.x, autoPaddle.speed * 1 * Time.deltaTime);
        }
        else if (diferecnia < 0)
        {
            autoPaddle.rb.velocity = new Vector2(autoPaddle.rb.velocity.x, autoPaddle.speed * -1 * Time.deltaTime);
        }
    }


}

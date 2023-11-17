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
    public void Update()
    {
        float diferencia = autoPaddle.startPosition.y - autoPaddle.transform.position.y;
        if (ball.velocity.x > 0)
        {
            autoPaddle.autoPaddleFMS.TransitionTo(autoPaddle.autoPaddleFMS.activeState);
        }


        if (autoPaddle.startPosition.y == autoPaddle.transform.position.y)
        {
            autoPaddle.rb.velocity = new Vector2(autoPaddle.rb.velocity.x, autoPaddle.speed * 0);
        }
        else
        {
            if (diferencia < 0)
            {
                autoPaddle.rb.velocity = new Vector2(autoPaddle.rb.velocity.x, autoPaddle.speed * -1);
            }
            else
            {
                autoPaddle.rb.velocity = new Vector2(autoPaddle.rb.velocity.x, autoPaddle.speed * 1);
            }
        }



    }


}

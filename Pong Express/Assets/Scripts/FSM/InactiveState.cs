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
        if (ball.velocity.x > 0)
        {
            autoPaddle.autoPaddleFSM.TransitionTo(autoPaddle.autoPaddleFSM.activeState);
        }
        float diferencia = autoPaddle.startPosition.y - autoPaddle.transform.position.y;

        if (diferencia < -0.1f)
        {
            autoPaddle.rb.velocity = new Vector2(autoPaddle.rb.velocity.x, autoPaddle.speed * -1);
        }
        else if (diferencia > .1f)
        {
            autoPaddle.rb.velocity = new Vector2(autoPaddle.rb.velocity.x, autoPaddle.speed * 1);
        }
        else
        {
            autoPaddle.rb.velocity = new Vector2(autoPaddle.rb.velocity.x, 0);
        }



    }


}

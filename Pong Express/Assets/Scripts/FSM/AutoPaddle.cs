using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPaddle : Paddle
{
    public AutoPaddleFMS autoPaddleFMS { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        autoPaddleFMS = new AutoPaddleFMS(this);
        autoPaddleFMS.Initialize(autoPaddleFMS.inactiveState);
        autoPaddleFMS.FixedUpdate();
        //autoPaddleFMS.Update();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPaddle : Paddle
{
    public AutoPaddleFMS autoPaddleFMS { get; private set; }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        autoPaddleFMS = new AutoPaddleFMS(this);
        autoPaddleFMS.Initialize(autoPaddleFMS.inactiveState);
    }

    void Update()
    {
        autoPaddleFMS.Update();
    }

}

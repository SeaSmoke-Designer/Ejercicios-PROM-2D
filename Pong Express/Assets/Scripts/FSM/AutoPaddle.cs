using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoPaddle : Paddle
{
    public AutoPaddleFSM autoPaddleFSM { get; private set; }
    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        autoPaddleFSM = new AutoPaddleFSM(this);
        autoPaddleFSM.Initialize(autoPaddleFSM.inactiveState);
    }

    void Update()
    {
        autoPaddleFSM.Update();
    }

}

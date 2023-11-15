using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class AutoPaddleFMS
{
    public IState CurrentState { get; private set; }

    // reference to the state objects
    public ActiveState activeState;
    public InactiveState inactiveState;

    // event to notify other objects of the state change
    //public event Action<IState> stateChanged;

    // pass in necessary parameters into constructor 
    public AutoPaddleFMS(AutoPaddle paddle)
    {
        // create an instance for each state and pass in PlayerController
        this.activeState = new ActiveState(paddle);
        this.inactiveState = new InactiveState(paddle);
    }

    // set the starting state
    public void Initialize(IState state)
    {
        CurrentState = state;
        state.Enter();


    }

    // exit this state and enter another
    public void TransitionTo(IState nextState)
    {
        CurrentState.Exit();
        CurrentState = nextState;
        nextState.Enter();


    }

    // allow the StateMachine to update this state
    public void FixedUpdate()
    {
        if (CurrentState != null)
        {
            CurrentState.Update();
        }
    }
}


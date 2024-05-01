using UnityEngine;

public abstract class State
{
    protected StateMachine stateMachine;
    protected References rf;

    public State(StateMachine stateMachine, References rf)
    {
        this.stateMachine = stateMachine;
        this.rf = rf;
    }
    public abstract void EnterState();
    public abstract void UpdateState();
    public abstract void ExitState();
}


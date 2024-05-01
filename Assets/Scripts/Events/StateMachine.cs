using UnityEngine;

public class StateMachine : MonoBehaviour
{
    private State currentState;
    protected References rf;

    public void Initialize(State startingState, References rf)
    {
        this.rf = rf;
        currentState = startingState;
        currentState.EnterState();
    }


    public void ChangeState(State newState)
    {
        if (currentState != null)
        {
            currentState.ExitState();
        }

        currentState = newState;
        currentState.EnterState();
    }

    void Update()
    {
        if (currentState != null)
        {
            currentState.UpdateState();
        }
    }
}

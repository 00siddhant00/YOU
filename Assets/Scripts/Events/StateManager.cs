using UnityEngine;

public class StateManager : MonoBehaviour
{
    private StateMachine stateMachine;
    References rf;

    private void Awake()
    {
        //print("Awake");
    }

    void Start()
    {
        //print("Start");
        stateMachine = GetComponent<StateMachine>();
        rf = GetComponent<References>();

        stateMachine.Initialize(new EnableDisable(stateMachine, rf), rf);
    }

}
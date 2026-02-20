using System;
using UnityEngine;

[Serializable]
public class FSM
{
    public string currentStateName;

    EnemyEntity owner;

    [SerializeField] State currentState;

    public void Init(EnemyEntity _owner, State _firstState)
    {
        owner = _owner;
        currentState = _firstState;

        currentStateName = currentState.StateName;
    }

    public void UpdateFSM()
    {
        if (!currentState.isStarted)
            currentState.Start(owner);

        currentState.Update(owner);

        foreach (Transition _transition in currentState.allTransitions)
        {
            if (_transition.requirement())
            {
                currentState.Stop(owner);

                currentState = _transition.nextState;
                currentStateName = currentState.StateName;
            }
        }
    }
}

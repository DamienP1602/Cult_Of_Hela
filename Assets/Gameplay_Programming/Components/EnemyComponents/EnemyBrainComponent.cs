using System.Collections.Generic;
using UnityEngine;

public class EnemyBrainComponent : MonoBehaviour
{
    [SerializeField] FSM fsm;
    [SerializeField] bool isFsmInitialized;

    public void InitBrain(EnemyEntity _owner)
    {
        fsm.Init(_owner, GenerateStates(_owner));
        isFsmInitialized = true;
    }

    State GenerateStates(EnemyEntity _owner)
    {
        // Get all Components we need
        EnemyDetectionComponent _detectionComponent = _owner.DetectionComponent;

        // Create all States
        State _idleState = new IdleState("Idle State");
        State _chaseState = new ChaseState("Chase State");

        // Create all transitions then give them to the states
        List<Transition> _idleTransitions = new List<Transition>()
        {
            new Transition(_chaseState,_detectionComponent.IsPlayerInDetectionRange)
        };
        _idleState.allTransitions = _idleTransitions;

        List<Transition> _chaseTransitions = new List<Transition>() 
        {
            new Transition(_idleState, () => !_detectionComponent.IsPlayerInDetectionRange())
        };
        _chaseState.allTransitions = _chaseTransitions;

        // Return the first State
        return _idleState;
    }

    // Update is called once per frame
    void Update()
    {
        if (isFsmInitialized)
            fsm.UpdateFSM();
    }
}

using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    MovementComponent MovementComponent;
    DetectionComponent DetectionComponent;
    InteractionComponent InteractionComponent;

    float currentTime = 0.0f;
    float maxTime = 0.5f;

    public ChaseState(string _stateName) : base(_stateName) { }

    public override void Start(EnemyEntity _owner)
    {
        MovementComponent = _owner.MovementComponent;
        DetectionComponent = _owner.DetectionComponent;
        InteractionComponent = _owner.InteractionComponent;

        currentTime = 0.0f;

        isStarted = true;
    }

    public override void Update(EnemyEntity _owner)
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            currentTime = 0.0f;
            UpdateDestination();
        }
    }

    public override void Stop(EnemyEntity _owner)
    {
        InteractionComponent.ResetTarget();
        DetectionComponent.ResetTarget();

        isStarted = false;
    }

    void UpdateDestination()
    {
        MovementComponent.SetDestination(DetectionComponent.Target.transform.position);
        InteractionComponent.SetTarget(DetectionComponent.Target);
    }
}

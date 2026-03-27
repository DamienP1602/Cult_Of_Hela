using UnityEngine;

public class ChaseState : State
{
    MovementComponent MovementComponent;
    EnemyDetectionComponent DetectionComponent;
    InteractionComponent InteractionComponent;

    float currentTime = 0.0f;
    float maxTime = 0.5f;

    public ChaseState(string _stateName) : base(_stateName) { }

    public override void Start(EnemyEntity _owner)
    {
        MovementComponent = _owner.MovementComponent;
        DetectionComponent = _owner.DetectionComponent;
        InteractionComponent = _owner.InteractionComponent;

        currentTime = 0.5f;
        isStarted = true;
    }

    public override void Update(EnemyEntity _owner)
    {
        currentTime += Time.deltaTime;
        if (currentTime >= maxTime)
        {
            currentTime = 0.0f;
            InteractionComponent.SetTarget(DetectionComponent.Target);
        }
    }

    public override void Stop(EnemyEntity _owner)
    {
        InteractionComponent.ResetTarget();
        DetectionComponent.ResetTarget();
        MovementComponent.ResetTarget();
        
        isStarted = false;
    }
}

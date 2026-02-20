using UnityEngine;

[RequireComponent(typeof(MovementComponent),typeof(InteractionComponent), typeof(StatsComponent))]
[RequireComponent(typeof(AttackComponent), typeof(AnimationComponent))]
public abstract class BaseEntity : GameEntity
{
    public MovementComponent MovementComponent { get; private set; }
    public InteractionComponent InteractionComponent { get; private set; }
    public StatsComponent StatsComponent { get; private set; }
    public AttackComponent AttackComponent { get; private set; }
    public AnimationComponent AnimationComponent { get; private set; }

    protected override void Start()
    {
        base.Start();

    }

    protected override void Init()
    {
        MovementComponent = GetComponent<MovementComponent>();
        InteractionComponent = GetComponent<InteractionComponent>();
        StatsComponent = GetComponent<StatsComponent>();
        AttackComponent = GetComponent<AttackComponent>();
        AnimationComponent = GetComponent<AnimationComponent>();
    }

    protected override void EventAssignation()
    {

    }
}

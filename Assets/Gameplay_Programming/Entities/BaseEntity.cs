using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(MovementComponent),typeof(InteractionComponent), typeof(StatsComponent))]
[RequireComponent(typeof(AttackComponent), typeof(AnimationComponent),typeof(SpellBookComponent))]
public abstract class BaseEntity : GameEntity
{
    public MovementComponent MovementComponent { get; private set; }
    public InteractionComponent InteractionComponent { get; private set; }
    public StatsComponent StatsComponent { get; private set; }
    public AttackComponent AttackComponent { get; private set; }
    public AnimationComponent AnimationComponent { get; private set; }
    public SpellBookComponent SpellBookComponent { get; private set; }

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
        SpellBookComponent = GetComponent<SpellBookComponent>();
    }

    protected override void EventAssignation()
    {
        StatsComponent.onDeath += EntityDeath;

        SpellBookComponent.OnLaunchSpell += () =>
        {
            AnimationComponent.SetBool("spell", true);
            AnimationComponent.LockAnimation("spell");
        };

        AttackComponent.OnLaunchAttack += () =>
        {
            AnimationComponent.SetBool("attack", true);
            AnimationComponent.LockAnimation("attack");
        };
    }

    protected virtual void EntityDeath()
    {
        AnimationComponent.SetTrigger("death");
        MovementComponent.GetComponent<NavMeshAgent>().enabled = false;
    }
}

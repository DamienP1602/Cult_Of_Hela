using UnityEngine;

[RequireComponent(typeof(SphereCollider), typeof(ProjectileMovementComponent),typeof(ProjectileOnHitComponent))]
public class ProjectileEntity : GameEntity
{
    public ProjectileMovementComponent MovementComponent { get; private set; }
    public ProjectileOnHitComponent OnHitComponent { get; private set; }

    private void Awake()
    {
        MovementComponent = GetComponent<ProjectileMovementComponent>();
        OnHitComponent = GetComponent<ProjectileOnHitComponent>();
    }

    protected override void EventAssignation()
    {

    }

    protected override void Init()
    {

    }

    private void Update()
    {

    }
}

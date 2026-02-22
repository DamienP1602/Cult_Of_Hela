using UnityEngine;

[RequireComponent(typeof(EnemyBrainComponent), typeof(EnemyDetectionComponent),typeof(DropComponent))]
public class EnemyEntity : BaseEntity
{
    public EnemyBrainComponent BrainComponent { get; private set; }
    public EnemyDetectionComponent DetectionComponent { get; private set; }
    public DropComponent DropComponent { get; private set; }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {
        
    }

    protected override void EventAssignation()
    {
        base.EventAssignation();
    }

    protected override void Init()
    {
        base.Init();

        BrainComponent = GetComponent<EnemyBrainComponent>();
        DetectionComponent = GetComponent<EnemyDetectionComponent>();
        DropComponent = GetComponent<DropComponent>();

        BrainComponent.InitBrain(this);
    }

    protected override void EntityDeath()
    {
        base.EntityDeath();

        BrainComponent.enabled = false;
        DropComponent.DropLootTable();

        CapsuleCollider _collider = GetComponent<CapsuleCollider>();
        if (_collider)
            _collider.enabled = false;
    }
}

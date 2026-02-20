using UnityEngine;

[RequireComponent(typeof(BrainComponent), typeof(DetectionComponent))]
public class EnemyEntity : BaseEntity
{
    public BrainComponent BrainComponent { get; private set; }
    public DetectionComponent DetectionComponent { get; private set; }

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

        BrainComponent = GetComponent<BrainComponent>();
        DetectionComponent = GetComponent<DetectionComponent>();

        BrainComponent.InitBrain(this);
    }
}

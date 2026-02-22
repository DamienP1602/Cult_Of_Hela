using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(DropComponent),typeof(AnimationComponent))]
public class ChestEntity : InteractableEntity
{
    public DropComponent DropComponent { get; private set; }
    public AnimationComponent AnimationComponent { get; private set; }

    [Header("Parameters")]
    [SerializeField] Transform upperParent;

    protected override void Start()
    {
        base.Start();
    }


    void Update()
    {

    }

    protected override void Init()
    {
        base.Init();

        DropComponent = GetComponent<DropComponent>();
        AnimationComponent = GetComponent<AnimationComponent>();
    }

    public override void OnInteraction(PlayerEntity _player)
    {
        AnimationComponent.SetTrigger("open");
        DropComponent.DropLootTable();

        BoxCollider _collider = GetComponent<BoxCollider>();
        if (_collider)
        {
            _collider.enabled = false;
        }
    }

    private void OnDrawGizmos()
    {

    }
}

using UnityEngine;

public abstract class InteractableEntity : GameEntity
{
    
    protected override void Start()
    {
        base.Start();
    }

    protected override void EventAssignation()
    {

    }

    protected override void Init()
    {

    }

    public virtual void OnInteraction(PlayerEntity _player)
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public abstract class GameEntity : MonoBehaviour
{
    [field: SerializeField] public string EntityName { get; private set; }

    [Header("Base Entity Parameters")]
    [SerializeField] protected bool initialized;
    [SerializeField] protected bool hasBeenDestroyed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        if (initialized) return;

        Init();
        EventAssignation();
        initialized = true;
    }

    protected abstract void Init();

    protected abstract void EventAssignation();
}

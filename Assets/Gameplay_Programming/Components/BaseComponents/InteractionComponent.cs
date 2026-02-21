using System;
using UnityEngine;

public class InteractionComponent : MonoBehaviour
{
    AttackComponent attackRef;
    MovementComponent movementRef;

    [Header("Debug")]
    [SerializeField] bool showDebug;

    [Header("Parameters")]
    [SerializeField] float interactionDistance = 1.0f;
    [SerializeField] GameEntity target;

    public GameEntity Target => target;

    private void Awake()
    {
        attackRef = GetComponent<AttackComponent>();
        movementRef = GetComponent<MovementComponent>();
    }

    void Start()
    {

    }

    void Update()
    {
        CheckForTarget();
    }

    private void OnDrawGizmos()
    {
        if (!showDebug) return;

        Gizmos.color = Color.red;

        Gizmos.DrawWireSphere(transform.position, interactionDistance);
    }

    void CheckForTarget()
    {
        if (IsTargetInInteractionRange())
        {
            ManageInteraction();
        }
    }

    public void SetTarget(GameEntity _entity)
    {
        target = _entity;

        if (IsTargetInInteractionRange())
            ManageInteraction();
        else
            movementRef.SetTarget(_entity);
    }

    public void ResetTarget() => target = null;

    void ManageInteraction()
    {
        if (target is BaseEntity _enemy)
        {
            attackRef.SetTarget(_enemy);
            movementRef.SetRotationTarget(_enemy.transform.position);
        }
        else if (target is InteractableEntity _interactable)
        {
            PlayerEntity _player = GetComponent<PlayerEntity>();
            _interactable.OnInteraction(_player);
        }

        movementRef.StopMovement();
        ResetTarget();
    }

    public bool IsTargetInInteractionRange()
    {
        if (!target) return false;

        return Vector3.Distance(target.transform.position, transform.position) <= interactionDistance;
    }

    public bool IsInRange(BaseEntity _entity)
    {
        return Vector3.Distance(_entity.transform.position, transform.position) <= interactionDistance;
    }
}

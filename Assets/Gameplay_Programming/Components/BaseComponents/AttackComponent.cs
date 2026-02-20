using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    AnimationComponent animRef;
    InteractionComponent interactRef;
    StatsComponent statsRef;

    [Header("Parameters")]
    [SerializeField] BaseEntity target;

    public void SetTarget(BaseEntity _entity)
    {
        target = _entity;
        animRef.SetBool("attack", true);
    }

    private void Awake()
    {
        animRef = GetComponent<AnimationComponent>();
        interactRef = GetComponent<InteractionComponent>();
        statsRef = GetComponent<StatsComponent>();
    }

    void Anim_Attack()
    {
        if (interactRef.IsInRange(target))
        {
            StatsComponent _targetStats = target.StatsComponent;

            _targetStats.LooseHealth(statsRef.damage.Value);
        }

        target = null;
    }
}

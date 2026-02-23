using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour, ICustomBonus<AttackBonusEffect>
{
    AnimationComponent animRef;
    InteractionComponent interactRef;
    StatsComponent statsRef;

    [Header("Parameters")]
    [SerializeField] BaseEntity target;
    [SerializeField] List<AttackBonusEffect> attackBonuses = new List<AttackBonusEffect>();

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

    private void Update()
    {
        UpdateCustomEffect();
    }

    public void UpdateCustomEffect()
    {
        foreach (AttackBonusEffect _bonus in attackBonuses)
        {
            if (_bonus.TimeEffectUpdate())
            {
                attackBonuses.Remove(_bonus);
                return;
            }
        }
    }

    public void AddEffect(AttackBonusEffect _effect) => attackBonuses.Add(_effect);

    public void RemoveEffect(AttackBonusEffect _effect) => attackBonuses.Remove(_effect);

    void Anim_Attack()
    {
        if (interactRef.IsInRange(target))
        {
            StatsComponent _targetStats = target.StatsComponent;
            if (DealDamage(_targetStats, statsRef.damage.Value))
                return;

            foreach (AttackBonusEffect _bonus in attackBonuses)
            {
                int _damageValue = (int)_bonus.ActivateEffect();
                if (DealDamage(_targetStats, _damageValue))
                    return;
            }
        }

        target = null;
    }

    /// <summary>
    /// return true = target is dead and we should stop dealing damages
    /// </summary>
    /// <param name="_targetStats"></param>
    /// <returns></returns>
    bool DealDamage(StatsComponent _targetStats, int _value)
    {
        _targetStats.LooseHealth(_value);

        if (_targetStats.IsDead)
            return true;

        return false;
    }
}

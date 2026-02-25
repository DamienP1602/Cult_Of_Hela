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
            _targetStats.LooseHealth(statsRef.damage.Value);

            int _size = attackBonuses.Count;
            for (int _i = 0; _i < _size; _i++)
            {
                AttackBonusEffect _bonus = attackBonuses[_i];
                int _damageValue = (int)_bonus.ActivateEffect();

                _targetStats.LooseHealth(_damageValue);
                if (_bonus.OneTimeUse)
                {
                    RemoveEffect(_bonus);
                    _i--;
                    _size = attackBonuses.Count;
                }

            }
        }

        target = null;
    }

    void DealDamage(StatsComponent _targetStats, int _value)
    {
        _targetStats.LooseHealth(_value);
    }
}

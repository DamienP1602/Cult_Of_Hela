using NUnit.Framework.Internal;
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
        animRef.LockAnimation("attack");
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

    public bool HasEffect(string _effectID)
    {
        foreach (AttackBonusEffect _bonus in attackBonuses)
        {
            if (_bonus.effectID == _effectID)
                return true;
        }
        return false;
    }

    void Anim_Attack()
    {
        if (interactRef.IsInRange(target))
        {
            StatsComponent _targetStats = target.StatsComponent;
            Vector3 _targetPos = target.transform.position + (Vector3.up * 2.0f);

            int _damageDeal = statsRef.GetDamageDeal();

            for (int _i = 0; _i < attackBonuses.Count; _i++)
            {
                AttackBonusEffect _bonus = attackBonuses[_i];
                _damageDeal += (int)_bonus.ActivateEffect();

                if (_bonus.OneTimeUse)
                {
                    RemoveEffect(_bonus);
                    _i--;
                }
            }

            _targetStats.LooseHealth(_damageDeal);
            if (target is EnemyEntity)
                WorldWidgetsManager.Instance.SpawnDamageText(_targetPos, _damageDeal);
        }

        target = null;
    }

    void DealDamage(StatsComponent _targetStats, int _value)
    {
        _targetStats.LooseHealth(_value);
    }
}

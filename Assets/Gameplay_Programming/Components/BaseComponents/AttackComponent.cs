using System;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public event Action OnLaunchAttack;

    InteractionComponent interactRef;
    StatsComponent statsRef;

    [Header("Parameters")]
    [SerializeField] BaseEntity target;
    [SerializeField] CustomEffectInterface<AttackBonusEffect> bonusEffects = new CustomEffectInterface<AttackBonusEffect>();

    public CustomEffectInterface<AttackBonusEffect> BonusEffects => bonusEffects;

    public void SetTarget(BaseEntity _entity)
    {
        target = _entity;
        OnLaunchAttack?.Invoke();
    }

    private void Awake()
    {
        interactRef = GetComponent<InteractionComponent>();
        statsRef = GetComponent<StatsComponent>();
    }

    private void Update()
    {
        bonusEffects.UpdateCustomEffect();
    }

    void Anim_Attack()
    {
        if (interactRef.IsInRange(target))
        {
            StatsComponent _targetStats = target.StatsComponent;
            Vector3 _targetPos = target.transform.position + (Vector3.up * 2.0f);

            int _damageDeal = statsRef.GetDamageDeal();

            for (int _i = 0; _i < bonusEffects.Count; _i++)
            {
                CustomEffectData<AttackBonusEffect> _data = bonusEffects.GetEffect(_i);
                _damageDeal += (int)_data.effect.ActivateEffect();

                if (_data.effect.OneTimeUse)
                {
                    bonusEffects.RemoveEffect(_data);
                    _i--;
                }
            }

            _targetStats.LooseHealth(_damageDeal);
        }

        target = null;
    }
}

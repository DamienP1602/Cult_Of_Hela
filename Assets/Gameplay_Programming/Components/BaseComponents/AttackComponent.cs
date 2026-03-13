using System;
using UnityEngine;

public class AttackComponent : MonoBehaviour
{
    public event Action OnLaunchAttack;

    InteractionComponent interactRef;
    StatsComponent statsRef;

    [Header("Parameters")]
    [SerializeField] BasicAttack basicAttackData;
    [SerializeField] BaseEntity target;
    [SerializeField] CustomEffectInterface<AttackBonusEffect> bonusEffects = new CustomEffectInterface<AttackBonusEffect>();

    public CustomEffectInterface<AttackBonusEffect> BonusEffects => bonusEffects;
    public BasicAttack BasicAttack => basicAttackData;

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

            // Va devoir changer pour récupérer les dégâts de l'arme
            int _damageDeal = statsRef.GetDamageDeal();
            _damageDeal += basicAttackData.GetBasicDamages();

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

            if (_targetStats.LooseHealth(_damageDeal))
            {
                if (GetComponent<PlayerLevelComponent>() is PlayerLevelComponent _playerLevel)
                {
                    EnemyEntity _enemy = target as EnemyEntity;
                    _playerLevel.GainExperience(_enemy.GetExperienceAmount());
                }
            }
        }

        target = null;
    }
}

using System.Collections.Generic;
using UnityEngine;

public enum SpellActionType
{
    AttackBonus,
    ThrowProjectile,
    MeleeAttack,
    StatsBonus,
    SpecialAttack
}

[CreateAssetMenu(fileName = "Spell", menuName = "Scriptable Objects/Abilities/Spell")]
public class Spell : Ability
{
    public SpellActionType spellAction;
    public GameObject objectReference;
    public int ressourceCost;
    public int cooldown;
    public int spellValue;
    public CustomEffect effect;
    public bool hasAnimation;
    public string animationName;

    public bool hasRequirement;
    public EquipmentSlotType EquipmentRequirement;

    public float strengthPercent;
    public float bonusAttackPercent;
    public float bonusSpellPercent;
    public bool monoTarget;
    public float areaOfEffect;
    public bool inFront;

    public GameObject specialObjectToSpawn;

    /// <summary>
    /// if true, it means the spell has been casted
    /// </summary>
    /// <param name="_owner"></param>
    /// <returns></returns>
    public bool LaunchSpell(BaseEntity _owner)
    {
        switch (spellAction)
        {
            case SpellActionType.AttackBonus:
                return AttackBonus(_owner);

            case SpellActionType.ThrowProjectile:
                return LaunchProjectile(_owner);

            case SpellActionType.MeleeAttack:
                return DoMeleeAttack(_owner);

            case SpellActionType.StatsBonus:
                return GiveBonus(_owner);

            case SpellActionType.SpecialAttack:
                return SpecialAttack(_owner);
        }

        return false;
    }

    bool AttackBonus(BaseEntity _owner)
    {
        AttackBonusEffect _attackBonus = effect as AttackBonusEffect;
        if (_attackBonus.OneTimeUse)
        {
            if (_owner.AttackComponent.BonusEffects.HasEffect(AbilityID))
            {
                _owner.AttackComponent.BonusEffects.RemoveEffect(AbilityID);
            }
        }

        _owner.AttackComponent.BonusEffects.AddEffect(_attackBonus);
        return true;
    }

    bool LaunchProjectile(BaseEntity _owner)
    {
        Vector3 _startPos = _owner.transform.position + Vector3.up + _owner.transform.forward;
        GameObject _object = Instantiate(objectReference, _startPos, _owner.transform.rotation);
        ProjectileEntity _projectile = _object.GetComponent<ProjectileEntity>();
        _projectile.SetOwner(_owner);
        _projectile.OnHitComponent.InitOnHitEffects(spellValue, effect);

        return true;
    }

    bool DoMeleeAttack(BaseEntity _owner)
    {
        if (monoTarget)
        {
            RaycastHit[] _hits = Physics.RaycastAll(new Ray(_owner.transform.position + Vector3.up, _owner.transform.forward), _owner.InteractionComponent.Range);
            SearchHitResult<BaseEntity>? _searchedResult = Macro.GetComponentFromHit<BaseEntity>(_hits);

            if (_searchedResult?.component)
            {
                DealDamageTo(_owner, new List<BaseEntity>() { _searchedResult?.component });
            }
        }
        else
        {
            List<BaseEntity> _targets = new List<BaseEntity>();
            RaycastHit[] _multiHits = Physics.SphereCastAll(new Ray(_owner.transform.position, _owner.transform.forward), _owner.InteractionComponent.Range);
            foreach (RaycastHit _hit in _multiHits)
            {
                if (_hit.collider.GetComponent<BaseEntity>() is BaseEntity _entity)
                {
                    if (inFront)
                    {
                        float _dotValue = Vector3.Dot(_owner.transform.forward, _entity.transform.forward);

                        if (_dotValue > 0.0f) continue;
                    }
                    _targets.Add(_entity);
                }
            }
            DealDamageTo(_owner, _targets);
        }

        return true;
    }

    void DealDamageTo(BaseEntity _owner, List<BaseEntity> _entities)
    {
        int _damage = GetDamages(_owner);

        foreach (BaseEntity _entity in _entities)
        {
            if (_entity.StatsComponent.LooseHealth(_damage))
            {
                if (_owner.GetComponent<PlayerLevelComponent>() is PlayerLevelComponent _playerLevel)
                {
                    EnemyEntity _enemy = _entity as EnemyEntity;
                    _playerLevel.GainExperience(_enemy.GetExperienceAmount());
                }
            }
        }
    }

    public int GetDamages(BaseEntity _owner)
    {
        if (spellAction == SpellActionType.AttackBonus || spellAction == SpellActionType.StatsBonus || spellAction == SpellActionType.SpecialAttack)
        {
            return (int)effect.ActivateEffect(_owner.StatsComponent, _owner.GetComponent<PlayerLevelComponent>());
        }

        float _strengthValue = (strengthPercent * _owner.StatsComponent.strength.Value) / 100.0f;
        float _attackBonusValue = (bonusAttackPercent * _owner.StatsComponent.BonusAttack) / 100.0f;
        float _spellBonusValue = (bonusSpellPercent * _owner.StatsComponent.BonusSpell) / 100.0f;


        return (int)(_strengthValue + _attackBonusValue + _spellBonusValue);
    }

    bool GiveBonus(BaseEntity _owner)
    {
        if (effect.uniqueEffect)
        {
            if (_owner.StatsComponent.StatsBonuses.HasEffect(AbilityID))
            {
                _owner.StatsComponent.RemoveBonuses(effect as BonusStatsEffect);
                _owner.StatsComponent.StatsBonuses.RemoveEffect(AbilityID);
            }
        }

        _owner.StatsComponent.AddBonuses(effect as BonusStatsEffect);

        return true;
    }

    bool SpecialAttack(BaseEntity _owner)
    {
        if (_owner.GetComponentInChildren<BladeStormComponent>() is BladeStormComponent _skillEffect)
        {
            Destroy(_skillEffect.gameObject);
        }

        GameObject _obj = Instantiate(specialObjectToSpawn, _owner.transform);

        if (_obj.GetComponent<BladeStormComponent>() is BladeStormComponent _storm)
        {
            _storm.Init(effect as AttackBonusEffect,_owner,_owner.InteractionComponent.Range);
        }

        return true;
    }

    public override bool Requirement(BaseEntity _owner)
    {
        if (hasRequirement)
        {
            PlayerEntity _player = _owner as PlayerEntity;
            return _player.EquipmentComponent.HasEquipmentAt(EquipmentRequirement);
        }
        return true;
    }
}

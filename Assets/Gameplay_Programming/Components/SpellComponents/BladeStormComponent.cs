using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class BladeStormComponent : MonoBehaviour
{
    [Serializable]
    class BladeStormData
    {
        public BaseEntity entity;
        public float duration;

        public BladeStormData(BaseEntity _entity)
        {
            entity = _entity;
            duration = 1.0f;
        }
    }

    [Header("Parameters")]
    [SerializeField] SphereCollider areaOfEffect;
    [SerializeField] List<BladeStormData> enemiesInArea = new List<BladeStormData>();
    [SerializeField] AttackBonusEffect damage;
    [SerializeField] BaseEntity owner;
    float currentRotation = 0.0f;

    void Start()
    {

    }

    void Update()
    {
        foreach (BladeStormData _data in enemiesInArea)
        {
            _data.duration += Time.deltaTime;
            if (_data.duration >= 1.0f)
            {
                if (_data.entity.StatsComponent.LooseHealth((int)damage.ActivateEffect(owner.StatsComponent, owner.GetComponent<PlayerLevelComponent>())))
                {
                    if (owner.GetComponent<PlayerLevelComponent>() is PlayerLevelComponent _playerLevel)
                    {
                        EnemyEntity _enemy = _data.entity as EnemyEntity;
                        _playerLevel.GainExperience(_enemy.GetExperienceAmount());

                        RemoveFromList(_enemy);
                        return;
                    }
                }
                _data.duration = 0.0f;
            }
        }

        Rotate();
    }

    void Rotate()
    {
        currentRotation += Time.deltaTime * 360.0f * 4.0f;
        owner.transform.rotation = Quaternion.AngleAxis(currentRotation, -transform.up);
    }

    public void Init(AttackBonusEffect _damage, BaseEntity _owner, float _range)
    {
        damage = _damage;
        owner = _owner;
        areaOfEffect.radius = _range;
        owner.AnimationComponent.SetLockAnimationState(true);

        Invoke(nameof(StopAbility), 6.0f);
    }

    void OnTriggerEnter(Collider _other)
    {
        BaseEntity _entity = _other.GetComponent<BaseEntity>();
        if (_entity)
            enemiesInArea.Add(new BladeStormData(_entity));
    }

    private void OnTriggerExit(Collider _other)
    {
        BaseEntity _entity = _other.GetComponent<BaseEntity>();
        if (_entity)
            RemoveFromList(_entity);
    }

    void RemoveFromList(BaseEntity _entity)
    {
        foreach (BladeStormData _data in enemiesInArea)
        {
            if (_data.entity == _entity)
            {
                enemiesInArea.Remove(_data);
                return;
            }
        }
    }

    void StopAbility()
    {
        Destroy(gameObject);
        owner.AnimationComponent.SetLockAnimationState(false);
        owner.AnimationComponent.SetBool(owner.AnimationComponent.CurrentSpellAnimName,false);

        if (!owner.MovementComponent.AtDestination)
        {
            owner.AnimationComponent.SetBool("movement", true);
        }
    }
}

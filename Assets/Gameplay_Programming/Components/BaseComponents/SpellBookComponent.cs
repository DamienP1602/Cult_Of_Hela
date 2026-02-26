using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookComponent : MonoBehaviour
{
    public event Action OnLaunchSpell;

    [SerializeField] List<Ability> allLearnedAbilities;
    [SerializeField] List<Spell> bindedSpells;
    Spell currentSpell;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void LaunchAbility(int _index)
    {
        // Don't have the selected spell
        if (_index >= bindedSpells.Count) return;
        currentSpell = bindedSpells[_index];

        StatsComponent _ownerStats = GetComponent<StatsComponent>();
        // can't use the spell : not enough ressources
        if (currentSpell.ressourceCost > _ownerStats.ressource.Value) return;

        if (currentSpell.hasAnimation)
        {
            MovementComponent _movement = GetComponent<MovementComponent>();

            // Launch spell from animation
            OnLaunchSpell?.Invoke();

            if (GetComponent<PlayerEntity>() is PlayerEntity _entity)
            {
                Vector3 _mousePos = _entity.ClickComponent.GetMousePositionOnWorld();
                if (_mousePos != Vector3.zero)
                {
                    _movement.SetRotationTarget(_mousePos);
                }
            }
            _movement.StopMovement();
        }
        else
        {
            BaseEntity _owner = GetComponent<BaseEntity>();
            if (currentSpell.LaunchSpell(_owner))
            {
                // Consume ressource
                _ownerStats.ressource.RemoveValue(currentSpell.ressourceCost);
            }
        }
    }

    void Anim_StartSpell()
    {
        StatsComponent _ownerStats = GetComponent<StatsComponent>();

        BaseEntity _owner = GetComponent<BaseEntity>();
        if (currentSpell.LaunchSpell(_owner))
        {
            // Consume ressource
            _ownerStats.ressource.RemoveValue(currentSpell.ressourceCost);
        }
    }
}

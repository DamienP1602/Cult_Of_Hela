using System;
using System.Collections.Generic;
using UnityEngine;

public class SpellBookComponent : MonoBehaviour
{
    [Serializable]
    public struct SpellLearnedData
    {
        public int levelRequired;
        public Spell spellToUnlock;
    }

    public event Action<Spell> OnLaunchSpell;
    public event Action OnLearnSpell;

    [Header("Parameters")]
    [SerializeField] List<Ability> allLearnedAbilities = new List<Ability>();
    [SerializeField] List<Spell> bindedSpells = new List<Spell>();
    [SerializeField] List<SpellLearnedData> learnableSpells = new List<SpellLearnedData>();
    Spell currentSpell;

    public List<Spell> Spells => bindedSpells;
    public List<SpellLearnedData> LearnableSpells => learnableSpells;

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

        BaseEntity _owner = GetComponent<BaseEntity>();
        StatsComponent _ownerStats = _owner.StatsComponent;

        if (!currentSpell.Requirement(_owner)) return;

        // can't use the spell : not enough ressources
        if (currentSpell.ressourceCost > _ownerStats.ressource.Value) return;

        if (currentSpell.hasAnimation)
        {
            MovementComponent _movement = GetComponent<MovementComponent>();

            // Launch spell from animation
            OnLaunchSpell?.Invoke(currentSpell);

            // If were the player, we rotate to the mouse position from screen to world
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

    public void CheckLevelToLearn(int _level)
    {
        foreach (SpellLearnedData _data in learnableSpells)
        {
            if (_data.levelRequired == _level)
            {
                allLearnedAbilities.Add(_data.spellToUnlock);

                if (bindedSpells.Count < 4)
                {
                    bindedSpells.Add(_data.spellToUnlock);
                    OnLearnSpell?.Invoke();
                }

                return;
            }
        }
    }

    public void AddSpell(Spell _spell)
    {
        if (bindedSpells.Count < 4)
        {
            bindedSpells.Add(_spell);
            OnLearnSpell?.Invoke();
        }
    }

    public void RemoveSpell(Spell _spell)
    {
        bindedSpells.Remove(_spell);
        OnLearnSpell?.Invoke();
    }

    public bool ParseSpell(out Spell _spell, int _index)
    {
        if (_index < 0 || _index > bindedSpells.Count)
        {
            _spell = null;
            return false;
        }

        _spell = bindedSpells[_index];
        return true;
    }
}

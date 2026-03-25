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

    [Serializable]
    public class BindedSpellsData
    {
        public Spell spell;
        public float currentCooldown;
        public float spellCooldown;

        public BindedSpellsData(Spell _spell, float _cooldown)
        {
            spell = _spell;
            spellCooldown = _cooldown;
        }
    }

    public event Action<Spell> OnLaunchSpell;
    public event Action<Spell,int> OnStartCooldown;
    public event Action OnLearnSpell;

    [Header("Parameters")]
    [SerializeField] List<Ability> allLearnedAbilities = new List<Ability>();
    [SerializeField] List<Spell> bindedSpells = new List<Spell>();
    [SerializeField] List<SpellLearnedData> learnableSpells = new List<SpellLearnedData>();
    [SerializeField] List<BindedSpellsData> cooldowns;
    Spell currentSpell;

    [field: SerializeField] public bool CanLaunchSpell { get; set; } = true;
    public List<Spell> Spells => bindedSpells;
    public List<SpellLearnedData> LearnableSpells => learnableSpells;

    void Start()
    {
        
    }

    void Update()
    {
        CooldownUpdate();
    }

    void CooldownUpdate()
    {
        foreach (BindedSpellsData _data in cooldowns)
        {
            _data.currentCooldown += Time.deltaTime;

            if (_data.currentCooldown >= _data.spellCooldown)
            {
                cooldowns.Remove(_data);
                return;
            }
        }
    }

    bool IsSpellInCooldown(Spell _spell)
    {
        foreach (BindedSpellsData _data in cooldowns)
        {
            if (_data.spell == _spell) return true;
        }
        return false;
    }

    public void LaunchAbility(int _index)
    {
        if (!CanLaunchSpell) return;

        // Don't have the selected spell
        if (_index >= bindedSpells.Count) return;

        // If spell is in cooldown
        if (IsSpellInCooldown(bindedSpells[_index]))
            return;

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
            LaunchSpell(_owner);
        }

        cooldowns.Add(new BindedSpellsData(currentSpell,currentSpell.cooldown));
        OnStartCooldown?.Invoke(currentSpell,_index);
    }

    void Anim_StartSpell()
    {
        StatsComponent _ownerStats = GetComponent<StatsComponent>();

        BaseEntity _owner = GetComponent<BaseEntity>();
        LaunchSpell(_owner);
    }

    void LaunchSpell(BaseEntity _entity)
    {
        if (currentSpell.LaunchSpell(_entity))
        {
            StatsComponent _ownerStats = _entity.StatsComponent;
            // Consume ressource
            _ownerStats.ressource.RemoveValue(currentSpell.ressourceCost);

            if (currentSpell.visualEffect)
            {
                VisualEffectComponent _effectComp = GetComponent<VisualEffectComponent>();
                _effectComp.CreateVisualEffect(currentSpell.visualEffect, currentSpell.EquipmentRequirement, 2.0f);
            }
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
        if (_index < 0 || _index >= bindedSpells.Count)
        {
            _spell = null;
            return false;
        }

        _spell = bindedSpells[_index];
        return true;
    }

    private void OnDrawGizmos()
    {
        if (currentSpell)
        {
            Gizmos.color = Color.blue;
            if (currentSpell.monoTarget)
            {
                Gizmos.DrawLine(transform.position, transform.position + transform.forward * GetComponent<InteractionComponent>().Range);
            }
            else
            {

                Gizmos.DrawWireSphere(transform.position,GetComponent<InteractionComponent>().Range);
            }
            Gizmos.color = Color.white;
        }
    }
}

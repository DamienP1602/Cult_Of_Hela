using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSpellsWidget : MonoBehaviour
{
    [SerializeField] List<SpellButtonWidget> spellButtons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitButtonSpells(PlayerEntity _player, SpellDescriptionWidget _spellDescriptionWidget)
    {
        int _count = spellButtons.Count;
        List<Spell> _playerSpells = _player.SpellBookComponent.Spells;

        for (int _i = 0; _i < _count; _i++)
        {
            SpellButtonWidget _widget =spellButtons[_i];

            _widget.Button.AddOnEnterAction(() => _player.ClickComponent.SetCanClick(false));
            _widget.Button.AddOnExitAction(() => _player.ClickComponent.SetCanClick(true));
            

            // _i = 0/1/2/3 is for spells
            // _spellCount > _i verify if there's a spell in this slot
            if (_i <= 3 && _playerSpells.Count > _i)
            {
                Spell _spell = _player.SpellBookComponent.Spells[_i];

                _widget.Init(_spell.abilitySprite, _spell.abilitySpriteColor,_i);
                _widget.Button.AddLeftClickAction(() => _player.SpellBookComponent.LaunchAbility(_widget.Index));

                _widget.Button.AddOnExitAction(() => _spellDescriptionWidget.HideSpellDescription());
                _widget.Button.AddHoverAction(() => _spellDescriptionWidget.ShowSpellDescription(_spell),0.5f);
            }

            // _i = 4 is for basic attack
            else if (_i == 4)
            {
                BasicAttack _attack = _player.AttackComponent.BasicAttack;

                _widget.Init(_attack.abilitySprite, _attack.abilitySpriteColor, 0);
                _widget.Button.AddLeftClickAction (() => _player.AttackComponent.ForceLaunchAttack());
            }
        }
    }
}

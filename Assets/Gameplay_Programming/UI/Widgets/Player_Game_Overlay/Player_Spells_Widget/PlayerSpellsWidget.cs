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

    public void InitButtonSpells(PlayerEntity _player)
    {
        int _spellIndex = 0;
        foreach (SpellButtonWidget _widget in spellButtons)
        {
            _widget.Button.AddOnEnterAction(() => _player.ClickComponent.SetCanClick(false));
            _widget.Button.AddOnExitAction(() => _player.ClickComponent.SetCanClick(true));

            Action _buttonAction;
            if (_spellIndex <= 3)
            {
                _buttonAction = () => _player.SpellBookComponent.LaunchAbility(_spellIndex);
            }
            else
            {
                _buttonAction = () => _player.AttackComponent.ForceLaunchAttack();
            }
            _widget.Button.AddLeftClickAction(_buttonAction);

            _spellIndex++;
        }
        // Spells

        Image _basicAttackSprite = spellButtons[4].SpellIcon;
        _basicAttackSprite.sprite = _player.AttackComponent.BasicAttack.abilitySprite;
        _basicAttackSprite.color = _player.AttackComponent.BasicAttack.abilitySpriteColor;
    }
}

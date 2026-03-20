using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerAbilitiesWidget : MonoBehaviour
{
    [Header("Basic Spells Parameters")]
    [SerializeField] List<SpellButtonWidget> basicSpells;
    [SerializeField] List<TMP_Text> basicSpellNames;

    [Header("Spell Description Parameters")]
    [SerializeField] SpellDescriptionWidget descriptionWidget;

    [Header("Passifs Parameters")]
    [SerializeField] List<SpecialisationPanelWidget> specialisationWidgets;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitAbilitiesWidget(PlayerEntity _player)
    {
        InitBasicSpells(_player);
    }

    void InitBasicSpells(PlayerEntity _player)
    {
        int _widgetCount = basicSpells.Count;
        for (int _i = 0; _i < _widgetCount; _i++)
        {
            SpellButtonWidget _widget = basicSpells[_i];
            TMP_Text _name = basicSpellNames[_i];

            if (_i == 0)
            {
                BasicAttack _basicAttack = _player.AttackComponent.BasicAttack;
                _widget.Init(_basicAttack.abilitySprite, _basicAttack.abilitySpriteColor);
                _name.text = "Basic Attack";

                _widget.Button.AddHoverAction(() => descriptionWidget.ShowSpellDescription(_basicAttack), 0.2f);
            }
            else
            {
                Spell _spell = _player.SpellBookComponent.LearnableSpells[_i - 1].spellToUnlock;
                int _levelToUnlock = _player.SpellBookComponent.LearnableSpells[_i - 1].levelRequired;

                _widget.Init(_spell.abilitySprite, _spell.abilitySpriteColor);

                bool _goodLevel = _player.LevelComponent.Level >= _levelToUnlock;

                string _str = _goodLevel ? _spell.AbilityName : "Level : " + _levelToUnlock.ToString();
                _name.text = _str;

                Color _color = _goodLevel ? Color.white : Color.red;
                _name.color = _color;

                _widget.Button.AddHoverAction(() => descriptionWidget.ShowSpellDescription(_spell), 0.2f);

                _widget.Button.AddLeftClickAction(() => ToggleSpell(_spell, _levelToUnlock, _player));
            }

            _widget.Button.AddOnEnterAction(() => MoveDescription(_widget.Button));
            _widget.Button.AddOnExitAction(descriptionWidget.HideSpellDescription);
        }
    }

    void MoveDescription(CustomButton _button)
    {
        RectTransform _buttonTransform = _button.GetComponent<RectTransform>();
        RectTransform _widgetTransform = descriptionWidget.GetComponent<RectTransform>();

        Vector3 _offset = new Vector3(_buttonTransform.rect.width / 2.0f, _buttonTransform.rect.height / 2.0f) + new Vector3(_widgetTransform.rect.width / 2.0f, _widgetTransform.rect.height / 2.0f);
        descriptionWidget.transform.position = _button.transform.position - _offset;
    }

    public void UpdateSpells()
    {
        PlayerEntity _player = GameManager.Instance.Player;

        int _widgetCount = basicSpells.Count;
        for (int _i = 0; _i < _widgetCount; _i++)
        {
            SpellButtonWidget _widget = basicSpells[_i];
            TMP_Text _name = basicSpellNames[_i];

            if (_i == 0)
                continue;

            Spell _spell = _player.SpellBookComponent.LearnableSpells[_i - 1].spellToUnlock;
            int _levelToUnlock = _player.SpellBookComponent.LearnableSpells[_i - 1].levelRequired;

            _widget.Init(_spell.abilitySprite, _spell.abilitySpriteColor);

            bool _cantLearnSpell = _player.LevelComponent.Level < _levelToUnlock;

            string _str = _cantLearnSpell ? "Level : " + _levelToUnlock.ToString() : _spell.AbilityName;
            _name.text = _str;

            Color _color = _cantLearnSpell ? Color.red : Color.white;
            _name.color = _color;
        }
    }

    public void ToggleSpell(Spell _spell,int _learnLevel, PlayerEntity _player)
    {
        if (_player.LevelComponent.Level < _learnLevel) return;

        if (_player.SpellBookComponent.Spells.Contains(_spell))
        {
            _player.SpellBookComponent.RemoveSpell(_spell);
        }
        else
        {
            _player.SpellBookComponent.AddSpell(_spell);
        }
    }
}

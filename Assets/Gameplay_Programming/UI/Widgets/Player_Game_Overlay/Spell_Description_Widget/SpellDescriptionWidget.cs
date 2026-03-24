using TMPro;
using UnityEngine;

public class SpellDescriptionWidget : MonoBehaviour
{
    [SerializeField] TMP_Text spellName;
    [SerializeField] TMP_Text spellDescription;
    [SerializeField] TMP_Text spellRessourceCost;
    [SerializeField] TMP_Text spellCooldown;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSpellDescription(Ability _ability)
    {
        if (!_ability) return; 

        gameObject.SetActive(true);

        spellName.text = _ability.AbilityName;
        if (_ability is Spell _spell)
        {
            spellDescription.text = GetSpellDescription(_spell);
            spellRessourceCost.text = "Ressource Cost : " + _spell.ressourceCost.ToString();
            spellCooldown.text = "Cooldown : " + _spell.cooldown.ToString();
        }
        else if (_ability is BasicAttack _attack)
        {
            spellDescription.text = GetBasicAttackDescription(_attack);
            spellRessourceCost.text = "";
            spellCooldown.text = "";
        }
    }

    public void HideSpellDescription()
    {
        gameObject.SetActive(false);
    }

    string GetSpellDescription(Spell _spell)
    {
        string _description = "";
        PlayerEntity _player = GameManager.Instance.Player;

        if (_spell.AbilityDescription.Contains("#D#"))
        {
            int _damageAmount = _spell.GetDamages(_player);
            _description = _spell.AbilityDescription.Replace("#D#", _damageAmount.ToString());
        }

        return _description;
    }

    string GetBasicAttackDescription(BasicAttack _attack)
    {
        PlayerEntity _player = GameManager.Instance.Player;

        int _damageAmount = _attack.GetBasicDamages(_player) + _player.StatsComponent.BonusAttack;

        return "Deals " + _damageAmount.ToString() + " Damages.";
    }
}

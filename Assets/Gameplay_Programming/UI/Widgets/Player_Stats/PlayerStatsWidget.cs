using TMPro;
using UnityEngine;

public class PlayerStatsWidget : MonoBehaviour
{
    [SerializeField] TMP_Text leveltext;
    [SerializeField] CustomSlider experienceSlider;
    [SerializeField] TMP_Text strengthText;
    [SerializeField] TMP_Text intelligenceText;
    [SerializeField] TMP_Text dexterityText;
    [SerializeField] TMP_Text vitalityText;
    [SerializeField] TMP_Text spiritText;

    [SerializeField] TMP_Text damageText;
    [SerializeField] TMP_Text armorText;
    [SerializeField] TMP_Text critRateText;
    [SerializeField] TMP_Text critDamageText;

    [SerializeField] TMP_Text basicDamageBonusText;
    [SerializeField] TMP_Text spellBonusText;

    public void RefreshValues()
    {
        PlayerEntity _player = GameManager.Instance.Player;

        leveltext.text = "Level " + _player.LevelComponent.Level.ToString();
        experienceSlider.SetValue(_player.LevelComponent.Experience, _player.LevelComponent.ExperienceCap);
        
        strengthText.text = _player.StatsComponent.strength.Value.ToString();
        intelligenceText.text = _player.StatsComponent.intelligence.Value.ToString();
        dexterityText.text = _player.StatsComponent.dexterity.Value.ToString();
        vitalityText.text = _player.StatsComponent.vitality.Value.ToString();
        spiritText.text = _player.StatsComponent.spirit.Value.ToString();

        MultipleStat _damageStats = _player.StatsComponent.damages;
        int _basicAttackDamage = _player.AttackComponent.BasicAttack.baseDamages;
        int _bonusBasicAttackAmount = _player.StatsComponent.strength.Value / 2;
        Vector2 _damageAmount = new Vector2(_damageStats.Value + _basicAttackDamage + _bonusBasicAttackAmount, _damageStats.MaxValue + _basicAttackDamage + _bonusBasicAttackAmount);
        damageText.text = ((int)_damageAmount.x).ToString() + " - " + ((int)_damageAmount.y).ToString();

        armorText.text = _player.StatsComponent.armor.Value.ToString();
        critRateText.text = "0 %";

        double _critDamageValue = 50.0 + (double)_player.StatsComponent.dexterity.Value / 2.0;
        critDamageText.text = _critDamageValue.ToString() + " %";

        basicDamageBonusText.text = _bonusBasicAttackAmount.ToString();
        spellBonusText.text = (_player.StatsComponent.intelligence.Value / 2).ToString();
    }
}

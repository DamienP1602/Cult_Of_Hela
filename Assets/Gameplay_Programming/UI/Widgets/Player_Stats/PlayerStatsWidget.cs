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
    }
}

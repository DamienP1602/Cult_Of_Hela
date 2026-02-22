using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerOverlayWidget : MonoBehaviour
{
    [Header("Ressources Parameters")]
    [SerializeField] CustomSlider healthBar;
    [SerializeField] CustomSlider ressourceBar;

    public void ChangeHealthBar(int _value,int _maxValue)
    {
        healthBar.SetValue(_value, _maxValue);
    }

    public void ChangeRessourceBar(int _value, int _maxValue)
    {
        ressourceBar.SetValue(_value, _maxValue);
    }
}

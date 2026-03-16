using UnityEngine;

public class PlayerRessourcesWidget : MonoBehaviour
{
    [SerializeField] CustomSlider healthBar;
    [SerializeField] CustomSlider ressourceBar;
    [SerializeField] CustomSlider experienceBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeHealthBar(int _value, int _maxValue)
    {
        healthBar.SetGoalValue(_value, _maxValue);
    }

    public void ChangeRessourceBar(int _value, int _maxValue)
    {
        ressourceBar.SetGoalValue(_value, _maxValue);
    }

    public void ChangeExperienceBar(int _value, int _maxValue)
    {
        experienceBar.SetGoalValue(_value, _maxValue);
    }
}

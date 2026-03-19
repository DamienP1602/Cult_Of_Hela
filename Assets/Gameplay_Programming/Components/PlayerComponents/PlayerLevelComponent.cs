using System;
using UnityEngine;

public class PlayerLevelComponent : MonoBehaviour
{
    public event Action<int, int> OnGainExperience;
    public event Action<int> OnGainLevel;

    [Header("Parameters")]
    [SerializeField] int currentLevel;
    [SerializeField] int currentExperience;
    [SerializeField] int experienceCap;

    public int Level => currentLevel;
    public int Experience => currentExperience;
    public int ExperienceCap => experienceCap;

    public void GainExperience(int _experience)
    {
        currentExperience += _experience;

        if (currentExperience >= experienceCap)
        {
            int _remainingExperience = currentExperience - experienceCap;
            currentLevel++;

            OnGainLevel?.Invoke(currentLevel);
            CalculateExperienceCap();
            currentExperience = 0;
            GainExperience(_remainingExperience);
            return;
        }

        OnGainExperience?.Invoke(currentExperience, experienceCap);
    }

    void CalculateExperienceCap()
    {
        experienceCap = (currentLevel * 2) * (currentLevel * 5);
    }
}

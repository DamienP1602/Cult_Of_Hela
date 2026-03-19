using System;
using UnityEngine;

[Flags,Serializable]
public enum StatBonus
{
    BonusStrength
}

[CreateAssetMenu(fileName = "Bonus Stats Effect", menuName = "Scriptable Objects/CustomEffect/Bonus Stats")]
public class BonusStatsEffect : CustomEffect
{
    [Header("Stats to Boost")]
    public StatBonus statToBoost;

    [Header("Bonus Stats Parameters")]
    public float spellPowerPercent;
    public float levelMultiplier;

    public BonusStatsEffect(string _effectID,float _duration,bool _uniqueEffect, float _spellPower, float _levelMult) : base(_effectID,_duration,_uniqueEffect)
    {
        spellPowerPercent = _spellPower;
        levelMultiplier = _levelMult;
    }

    public override float ActivateEffect(StatsComponent _statOwner, PlayerLevelComponent _level)
    {
        float _spellValue = (_statOwner.BonusSpell * spellPowerPercent) / 100.0f;
        float _levelValue = _level.Level * levelMultiplier;

        return _spellValue + _levelValue;
    }
}

using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Bonus", menuName = "Scriptable Objects/CustomEffect/Attack Bonus")]
public class AttackBonusEffect : CustomEffect
{
    [Header("Bonus Attack Parameters")]
    public bool oneTimeUse;

    [Header("Damages Parameters")]
    public float strengthPercent;
    public float spellPowerPercent;
    public float attackBonusPercent;
     
    public bool OneTimeUse => oneTimeUse;

    public AttackBonusEffect(string _effectID, float _duration,bool _uniqueEffect, float _strengthPercent,float _spellPowerPercent,float _attackBonusPercent, bool _oneTimeUse) : base(_effectID,_duration, _uniqueEffect)
    {
        strengthPercent = _strengthPercent;
        spellPowerPercent = _spellPowerPercent;
        attackBonusPercent = _attackBonusPercent;
        oneTimeUse = _oneTimeUse;
    }

    public override float ActivateEffect(StatsComponent _statOwner, PlayerLevelComponent _level)
    {
        float _strengthValue = (_statOwner.strength.Value * strengthPercent) / 100.0f;
        float _spellPowerValue = (_statOwner.BonusSpell * spellPowerPercent) / 100.0f;
        float _attackBonusValue = (_statOwner.BonusAttack * attackBonusPercent) / 100.0f;

        return _strengthValue + _spellPowerValue + _attackBonusValue;
    }
}

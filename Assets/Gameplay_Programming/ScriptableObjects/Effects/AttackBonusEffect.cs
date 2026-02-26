using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack Bonus", menuName = "Scriptable Objects/CustomEffect/Attack Bonus")]
public class AttackBonusEffect : CustomEffect
{
    public int bonusDamage;
    public bool oneTimeUse;

    public bool OneTimeUse => oneTimeUse;

    public AttackBonusEffect(string _effectID, float _duration,bool _uniqueEffect, int _damageBoost, bool _oneTimeUse) : base(_effectID,_duration, _uniqueEffect)
    {
        bonusDamage = _damageBoost;
        oneTimeUse = _oneTimeUse;
    }

    public override float ActivateEffect()
    {
        return bonusDamage;
    }
}

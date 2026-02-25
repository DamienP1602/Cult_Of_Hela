using System;
using UnityEngine;

[Serializable]
public class AttackBonusEffect : CustomEffect
{
    [SerializeField] int BonusDamage;
    [SerializeField] bool oneTimeUse;

    public bool OneTimeUse => oneTimeUse;

    public AttackBonusEffect(string _effectID, float _duration,bool _uniqueEffect, int _damageBoost, bool _oneTimeUse) : base(_effectID,_duration, _uniqueEffect)
    {
        BonusDamage = _damageBoost;
        oneTimeUse = _oneTimeUse;
    }

    public override float ActivateEffect()
    {
        return BonusDamage;
    }
}

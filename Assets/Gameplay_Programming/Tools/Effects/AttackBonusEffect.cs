using System;
using UnityEngine;

[Serializable]
public class AttackBonusEffect : CustomEffect
{
    [SerializeField] int BonusDamage;

    public AttackBonusEffect(float _duration, int _damageBoost) : base(_duration)
    {
        BonusDamage = _damageBoost;
    }

    public override float ActivateEffect()
    {
        return BonusDamage;
    }
}

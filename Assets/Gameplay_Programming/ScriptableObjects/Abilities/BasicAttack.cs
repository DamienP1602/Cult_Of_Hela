using UnityEngine;

[CreateAssetMenu(fileName = "Basic Attack", menuName = "Scriptable Objects/Abilities/Basic Attack")]
public class BasicAttack : Ability
{
    public int baseDamages;

    public int bonusAttackPercent;

    public int GetBasicDamages(BaseEntity _owner)
    {
        float _attackBonusValue = (bonusAttackPercent * _owner.StatsComponent.BonusAttack) / 100.0f;
        return baseDamages + (int)_attackBonusValue;
    }

    public override bool Requirement(BaseEntity _owner)
    {
        return true;
    }
}

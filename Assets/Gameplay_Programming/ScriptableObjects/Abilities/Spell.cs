using UnityEngine;

public enum SpellActionType
{
    AttackBonus,
    ThrowProjectile
}

[CreateAssetMenu(fileName = "Spell", menuName = "Scriptable Objects/Abilities/Spell")]
public class Spell : Ability
{
    public SpellActionType spellAction;
    public GameObject objectReference;
    public int ressourceCost;
    public int cooldown;
    public int bonusValue;
    public bool hasDuration;
    public int bonusDuration;
    public bool oneTimeUse;
    public bool uniqueEffect;
    public bool hasAnimation;
    public string animationName;

    /// <summary>
    /// if true, it means the spell has been casted
    /// </summary>
    /// <param name="_owner"></param>
    /// <returns></returns>
    public bool LaunchSpell(BaseEntity _owner)
    {
        switch (spellAction)
        {
            case SpellActionType.AttackBonus:
                return AttackBonus(_owner);

            case SpellActionType.ThrowProjectile:
                break;

            default:
                break;
        }

        return false;
    }

    bool AttackBonus(BaseEntity _owner)
    {
        if (oneTimeUse)
        {
            if (_owner.AttackComponent.HasEffect(AbilityID))
                return false;
        }

        AttackBonusEffect _newEffect = new AttackBonusEffect(AbilityID,hasDuration ? bonusDuration : -1, uniqueEffect, bonusValue,oneTimeUse);
        _owner.AttackComponent.AddEffect(_newEffect);

        return true;
    }
}

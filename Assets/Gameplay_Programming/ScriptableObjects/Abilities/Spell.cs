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

    public void LaunchSpell(BaseEntity _owner)
    {
        switch (spellAction)
        {
            case SpellActionType.AttackBonus:
                AttackBonus(_owner);
                break;

            case SpellActionType.ThrowProjectile:
                break;

            default:
                break;
        }
    }

    void AttackBonus(BaseEntity _owner)
    {
        AttackBonusEffect _newEffect = new AttackBonusEffect(AbilityID,hasDuration ? bonusDuration : -1, uniqueEffect, bonusValue,oneTimeUse);
        _owner.AttackComponent.AddEffect(_newEffect);
    }
}

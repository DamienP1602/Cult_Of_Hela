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
    public int spellValue;
    public CustomEffect effect;
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
                return LaunchProjectile(_owner);

            default:
                break;
        }

        return false;
    }

    bool AttackBonus(BaseEntity _owner)
    {
        AttackBonusEffect _attackBonus = effect as AttackBonusEffect;
        if (_attackBonus.OneTimeUse)
        {
            if (_owner.AttackComponent.BonusEffects.HasEffect(AbilityID))
                return false;
        }

        _owner.AttackComponent.BonusEffects.AddEffect(_attackBonus);
        return true;
    }

    bool LaunchProjectile(BaseEntity _owner)
    {
        Vector3 _startPos = _owner.transform.position + Vector3.up + _owner.transform.forward;
        GameObject _object = Instantiate(objectReference, _startPos, _owner.transform.rotation);
        ProjectileEntity _projectile = _object.GetComponent<ProjectileEntity>();
        _projectile.OnHitComponent.InitOnHitEffects(spellValue, effect);

        return true;
    }
}

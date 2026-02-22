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

    public void LaunchSpell(BaseEntity _owner)
    {

    }
}

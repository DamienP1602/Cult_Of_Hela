using UnityEngine;

[CreateAssetMenu(fileName = "Spell", menuName = "Scriptable Objects/Abilities/Spell")]
public class Spell : Ability
{
    public enum SpellActionType
    {
        AttackBonus,
        ThrowProjectile
    }

    [Header("Spell Parameters")]
    public SpellActionType spellAction;
    public GameObject objectReference;
    public int ressourceCost;
    public int cooldown;
}

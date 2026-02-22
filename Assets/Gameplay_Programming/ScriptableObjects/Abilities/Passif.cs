using UnityEngine;

[CreateAssetMenu(fileName = "Passif", menuName = "Scriptable Objects/Abilities/Passif")]
public class Passif : Ability
{
    public enum SpellActionTypes
    {
        AttackBonus,
        ThrowProjectile
    }

    public SpellActionTypes spellAction;
    public GameObject objectReference;
    public int ressourceCost;
    public int cooldown;
}

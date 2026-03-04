using UnityEngine;

[CreateAssetMenu(fileName = "Basic Attack", menuName = "Scriptable Objects/Abilities/Basic Attack")]
public class BasicAttack : Ability
{
    public int baseDamages;

    public int GetBasicDamages()
    {
        return baseDamages;
    }
}

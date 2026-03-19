using UnityEngine;

[CreateAssetMenu(fileName = "Passif", menuName = "Scriptable Objects/Abilities/Passif")]
public class Passif : Ability
{
    public override bool Requirement(BaseEntity _owner)
    {
        return true;
    }
}

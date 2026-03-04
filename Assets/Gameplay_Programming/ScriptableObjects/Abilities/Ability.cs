using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [Header("Base Ability Parameters")]
    public string AbilityID;
    public string AbilityName;
    public string AbilityDescription;

    public Sprite abilitySprite;
    public Color abilitySpriteColor;
}

using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item/New Item")]
public class Item : ScriptableObject
{
    [Header("Base Parameters")]
    public string itemName;
    public Sprite itemIcon;

    [Header("Stats Parameters")]
    public Vector2 damages;
    public int armor;
    public int strength;
    public int intelligence;
    public int dexterity;
    public int vitality;
    public int spirit;   
}

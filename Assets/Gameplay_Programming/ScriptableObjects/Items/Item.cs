using System;
using UnityEngine;

[Serializable]
public enum ItemRarity
{
    Item_Common,
    Item_Rare,
    Item_Epic,
    Item_Legendary
}

[Serializable]
public enum EquipmentType
{
    Damage_Equipment,
    Defense_Equipment,
    Trinket_Equipment
}

[Serializable]
public enum ItemType
{
    Item_Sword,
    Item_Shield
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item/New Item")]
public class Item : ScriptableObject
{
    [Header("Base Parameters")]
    public string itemName;
    public Sprite itemIcon;

    [Header("Item Parameters")]
    public ItemType itemType;
    public EquipmentType equipmentType;
    public ItemRarity rarity;

    [Header("Stats Parameters")]
    public Vector2 damages;
    public int armor;
    public int strength;
    public int intelligence;
    public int dexterity;
    public int vitality;
    public int spirit;   
}

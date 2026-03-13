using System;
using System.Collections.Generic;
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
    Item_Great_Axe,
    Item_Shield
}

[Serializable]
public enum EquipmentSlotType
{
    Equipment_Right_Hand,
    Equipment_Left_Hand,
    Equipment_Head,
    Equipment_Body,
    Equipment_Leggings,
    Equipment_Boots,
    Equipment_Necklace,
    Equipment_Rings,
    Equipment_Trinket
}

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Objects/Item/New Item")]
public class Item : ScriptableObject
{
    [Header("Base Parameters")]
    public string itemName;
    public Sprite itemIcon;

    [Header("Graphic Parameters")]
    public Mesh mesh;
    public List<Material> materials;

    [Header("Item Parameters")]
    public ItemType itemType;
    public EquipmentType equipmentType;
    public ItemRarity rarity;
    public EquipmentSlotType equipmentSlotType;
    public bool twoHandItem;

    [Header("Stats Parameters")]
    public Vector2 damages;
    public int armor;
    public int strength;
    public int intelligence;
    public int dexterity;
    public int vitality;
    public int spirit;
}

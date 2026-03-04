using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemEquiped
{
    public ItemType type;
    public Item item;

    public ItemEquiped(ItemType _type, Item _item)
    {
        type = _type;
        item = _item;
    }
}

public class PlayerEquipmentComponent : MonoBehaviour
{
    [SerializeField] List<ItemEquiped> equipedItems = new List<ItemEquiped>();

    private void Awake()
    {
        Array _itemTypeList = Enum.GetValues(typeof(ItemType));

        foreach (object _type in _itemTypeList)
        {
            equipedItems.Add(new ItemEquiped((ItemType)_type, null));
        }
    } 
}

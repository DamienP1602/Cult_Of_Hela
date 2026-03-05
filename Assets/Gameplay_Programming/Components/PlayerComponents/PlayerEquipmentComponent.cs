using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemEquiped
{
    public EquipmentSlotType type;
    public Item item;

    public ItemEquiped(EquipmentSlotType _type, Item _item)
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
        Array _itemTypeList = Enum.GetValues(typeof(EquipmentSlotType));

        foreach (object _type in _itemTypeList)
        {
            equipedItems.Add(new ItemEquiped((EquipmentSlotType)_type, null));

            if ((EquipmentSlotType)_type == EquipmentSlotType.Equipment_Rings)
                equipedItems.Add(new ItemEquiped((EquipmentSlotType)_type, null));
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

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

    public void EquipItem(Item _item, EquipmentSlotType _slot)
    {
        foreach (ItemEquiped _equipedItem in equipedItems)
        {
            if (_slot == _equipedItem.type)
            {
                equipedItems.Remove(_equipedItem);
                equipedItems.Add(new ItemEquiped(_slot, _item));
                break;
            }
        }
    }

    public void DesequipItem(Item _item, EquipmentSlotType _slot)
    {
        foreach (ItemEquiped _equipedItem in equipedItems)
        {
            if (_slot == _equipedItem.type)
            {
                equipedItems.Remove(_equipedItem);
                equipedItems.Add(new ItemEquiped(_slot, null));
                break;
            }
        }
    }

    public bool HasEquipmentAt(EquipmentSlotType _slot)
    {
        foreach (ItemEquiped _equipedItem in equipedItems)
        {
            if (_equipedItem.type == _slot)
                return _equipedItem.item;
        }

        return false;
    }
}

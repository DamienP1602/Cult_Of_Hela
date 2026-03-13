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

    /// <summary>
    /// Send the new item to equip and the slot, if an item is returned that means an item was already in this slot and is going back to the inventory
    /// </summary>
    /// <param name="_item"></param>
    /// <param name="_slot"></param>
    /// <returns></returns>
    public Item EquipItem(Item _item, EquipmentSlotType _slot)
    {
        Item _temp = null;
        foreach (ItemEquiped _equipedItem in equipedItems)
        {
            if (_slot == _equipedItem.type)
            {
                if (_equipedItem.item)
                {
                    _temp = _equipedItem.item;

                }
                equipedItems.Remove(_equipedItem);
                equipedItems.Add(new ItemEquiped(_slot, _item));
                break;
            }
        }

        return _temp;
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
}

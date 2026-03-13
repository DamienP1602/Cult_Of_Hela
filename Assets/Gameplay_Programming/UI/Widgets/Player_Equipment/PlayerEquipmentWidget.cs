using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEquipmentWidget : MonoBehaviour
{
    public event Func<ItemInventoryData?> OnGetSelectedItem;
    public event Action<ItemSlotWidget, EquipmentSlotType> OnSelectWidget;
    public event Action<ItemInventoryData, EquipmentSlotType> OnItemEquiped;
    public event Action<ItemInventoryData, EquipmentSlotType> OnItemDesequip;

    [Serializable]
    struct EquipmentSlots
    {
        public EquipmentSlotType type;
        public ItemSlotWidget widget;
    }

    [SerializeField] List<EquipmentSlots> slots = new List<EquipmentSlots>();

    private void Awake()
    {
        foreach (EquipmentSlots _slot in slots)
        {
            _slot.widget.Button.AddLeftClickAction(() => SelectSlot(_slot));
        }
    }

    public void ChangeEquipmentInteractable(Item _itemSelected)
    {
        foreach (EquipmentSlots _slot in slots)
        {
            bool _isGoodEquipmentSlot = _slot.type == _itemSelected.equipmentSlotType;
            _slot.widget.SetButtonInteractable(_isGoodEquipmentSlot);
        }
    }

    public void ClearEquipmentInteractable()
    {
        foreach (EquipmentSlots _slot in slots)
        {
            _slot.widget.SetButtonInteractable(true);
        }
    }

    void SelectSlot(EquipmentSlots _slot)
    {
        ItemInventoryData? _selectedItem = OnGetSelectedItem?.Invoke();
        if (_slot.widget.IsUsed)
        {
            if (_selectedItem == null)
            {
                OnSelectWidget?.Invoke(_slot.widget, _slot.type);
                return;
            }

            ItemInventoryData _temp = _slot.widget.Item;
            EquipItem(_selectedItem.Value, _slot);
            OnItemDesequip?.Invoke(_temp, _slot.type);
            return;
        }

        if (_selectedItem != null)
            EquipItem(_selectedItem.Value, _slot);
    }

    void EquipItem(ItemInventoryData _selectedItem, EquipmentSlots _slot)
    {
        _slot.widget.InitSlot(_selectedItem);

        // if two hand is true => desactivate slot & put item in inventory
        // if two hand is false => activate the slot
        SetSecondHandStatus(_selectedItem.data.twoHandItem);

        OnItemEquiped?.Invoke(_selectedItem, _slot.type);
    }

    void SetSecondHandStatus(bool _value)
    {
        foreach (EquipmentSlots _slot in slots)
        {
            if (_slot.type == EquipmentSlotType.Equipment_Left_Hand)
            {
                if (_slot.widget.IsUsed)
                {
                    if (_value)
                    {
                        OnItemDesequip?.Invoke(_slot.widget.Item, EquipmentSlotType.Equipment_Left_Hand);
                        _slot.widget.ResetSlot();
                    }
                }

                _slot.widget.SetCloseValue(_value);
                return;
            }
        }
    }
}

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

    }

    public void Init()
    {
        foreach (EquipmentSlots _slot in slots)
        {
            _slot.widget.Button.AddLeftClickAction(() => SelectSlot(_slot));
            _slot.widget.Button.AddRightClickAction(() => AutoDesequipItem(_slot));
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
        // Check if we have a selected item
        ItemInventoryData? _selectedItem = OnGetSelectedItem?.Invoke();

        // if the slot we clicked on has already an item
        if (_slot.widget.IsUsed)
        {
            // if there's no item selected, we select the currently equiped item
            if (_selectedItem == null)
            {
                // if we have equiped a 2 hand weapon, we want to clear the second hand slot
                if (_slot.widget.Item.data.twoHandItem)
                    SetSecondHandStatus(false);

                OnSelectWidget?.Invoke(_slot.widget, _slot.type);
                return;
            }

            // here we have a selected item => put in a temp value the equiped item
            ItemInventoryData _temp = _slot.widget.Item;

            // we desequip the ancient equiped item
            OnItemDesequip?.Invoke(_temp, _slot.type);

            // we equip the new item
            EquipItem(_selectedItem.Value, _slot);
            return;
        }

        // the slot here don't have an item, if we have a selected item we equip it
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

    public void AutoEquipItem(ItemSlotWidget _slot)
    {
        EquipmentSlots? _equipmentSlot = SearchSlot(_slot.Item.data.equipmentSlotType);
        if (_equipmentSlot != null && !_equipmentSlot.Value.widget.IsClosed)
        {
            SelectSlot(_equipmentSlot.Value);
        }
    }

    EquipmentSlots? SearchSlot(EquipmentSlotType _type)
    {
        foreach (EquipmentSlots _slot in slots)
        {
            if (_slot.type == _type)
                return _slot;
        }

        return null;
    }

    void AutoDesequipItem(EquipmentSlots _slot)
    {
        if (_slot.widget.IsUsed)
        {
            // if we have equiped a 2 hand weapon, we want to clear the second hand slot
            if (_slot.widget.Item.data.twoHandItem)
                SetSecondHandStatus(false);

            OnItemDesequip(_slot.widget.Item, _slot.type);
            _slot.widget.ResetSlot();
        }
    }
}

using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEquipmentWidget : MonoBehaviour
{
    public event Func<Item> OnGetSelectedItem;

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
            _slot.widget.Button.AddLeftClickAction(() => EquipSelectedItem(_slot.widget));
        }
    }

    public void ChangeEquipmentInteractable(Item _itemSelected)
    {
        foreach (EquipmentSlots _slot in slots)
        {
            bool _isGoodEquipmentSlot = _slot.type == _itemSelected.equipmentSlotType;
            _slot.widget.Button.SetInteractable(_isGoodEquipmentSlot);
        }
    }

    public void ClearEquipmentInteractable()
    {
        foreach (EquipmentSlots _slot in slots)
        {
            _slot.widget.Button.SetInteractable(true);
        }
    }

    void EquipSelectedItem(ItemSlotWidget _widget)
    {
        Item _selectedItem = OnGetSelectedItem?.Invoke();
        if (!_selectedItem) return;

        //_widget.InitSlot(_selectedItem);
    }
}

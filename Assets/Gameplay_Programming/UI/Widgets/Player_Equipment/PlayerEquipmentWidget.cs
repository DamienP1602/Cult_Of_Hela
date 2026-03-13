using System;
using System.Collections.Generic;
using UnityEngine;


public class PlayerEquipmentWidget : MonoBehaviour
{
    public event Func<ItemInventoryData?> OnGetSelectedItem;
    public event Action<ItemSlotWidget,EquipmentSlotType> OnSelectWidget;
    public event Action<ItemInventoryData, EquipmentSlotType> OnItemEquiped;

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

    void SelectSlot(EquipmentSlots _slot)
    {
        ItemInventoryData? _selectedItem = OnGetSelectedItem?.Invoke();
        if (_selectedItem == null)
        {
            if (_slot.widget.IsUsed)
            {
                OnSelectWidget?.Invoke(_slot.widget,_slot.type);
            }
            return;
        }

        _slot.widget.InitSlot(_selectedItem.Value);
        OnItemEquiped?.Invoke(_selectedItem.Value, _slot.type);
    }
}

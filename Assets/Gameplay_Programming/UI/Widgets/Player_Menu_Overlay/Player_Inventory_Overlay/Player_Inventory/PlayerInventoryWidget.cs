using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerInventoryWidget : MonoBehaviour
{
    public event Action<ItemSlotWidget> OnSelectWidget;
    public event Action<ItemSlotWidget> OnAutoEquipItem;

    [SerializeField] List<ItemSlotWidget> allItemSlots;
    [SerializeField] TMP_Text goldText;

    public List<ItemSlotWidget> ItemSlots => allItemSlots;

    private void Awake()
    {
        allItemSlots = GetComponentsInChildren<ItemSlotWidget>(true).ToList();

        foreach (ItemSlotWidget _slot in allItemSlots)
        {
            _slot.Button.AddLeftClickAction(() => OnSelectWidget?.Invoke(_slot));
            _slot.Button.AddRightClickAction(() => OnAutoEquipItem?.Invoke(_slot));
        }
    }

    public void Init(List<ItemInventoryData> _items)
    {
        foreach (ItemSlotWidget _slot in allItemSlots)
        {
            _slot.ResetSlot();
        }

        int _size = _items.Count;
        for (int _i = 0; _i < _size; _i++)
        {
            ItemInventoryData _data = _items[_i];

            ItemSlotWidget _slot = allItemSlots[_data.inventoryPosition];
            _slot.InitSlot(_data);
        }
    }

    public void SetGoldText(int _goldAmount)
    {
        goldText.text = "Gold : " + _goldAmount.ToString();
    }

    public int GetIndexOfSlot(ItemSlotWidget _slot) => allItemSlots.FindIndex(s => s == _slot);
}

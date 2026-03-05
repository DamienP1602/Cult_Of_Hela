using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class PlayerInventoryWidget : MonoBehaviour
{
    public event Action<ItemSlotWidget> OnSelectWidget;

    [SerializeField] List<ItemSlotWidget> allItemSlots;
    [SerializeField] TMP_Text goldText;

    private void Awake()
    {
        allItemSlots = GetComponentsInChildren<ItemSlotWidget>(true).ToList();

        foreach (ItemSlotWidget _slot in allItemSlots)
        {
            _slot.Button.AddLeftClickAction(() => SelectItem(_slot));
        }
    }

    void SelectItem(ItemSlotWidget _slot)
    {
        if (!_slot.HasItem) return;

        Item _item = _slot.Item.data;
        _slot.ResetSlot();

        OnSelectWidget?.Invoke(_slot);
    }

    public void Init(List<ItemInventoryData> _items)
    {
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
}

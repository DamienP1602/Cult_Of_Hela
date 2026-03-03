using TMPro;
using UnityEngine;
using UnityEditor;
using System;
using System.Collections.Generic;
using System.Linq;

public class PlayerOverlayWidget : MonoBehaviour
{
    public event Func<List<ItemInventoryData>> OnOpenInventory;

    [Header("Ressources Parameters")]
    [SerializeField] CustomSlider healthBar;
    [SerializeField] CustomSlider ressourceBar;

    [SerializeField] GameObject inventory;
    [SerializeField] TMP_Text goldText;

    [SerializeField] List<ItemSlotWidget> allItemSlots;

    [SerializeField] ItemInformationWidget itemInformation;

    private void Awake()
    {
        allItemSlots = GetComponentsInChildren<ItemSlotWidget>(true).ToList();

        foreach (ItemSlotWidget _slot in allItemSlots)
        {
            Action _hoverAction = () =>
            {
                itemInformation.gameObject.SetActive(_slot.IsUsed);

                if (_slot.IsUsed)
                    itemInformation.Init(_slot.Item);
            };
            _slot.Button.AddHoverAction(_hoverAction, 0.1f);

            _slot.Button.AddOnExitAction(() => itemInformation.gameObject.SetActive(false));
        }
    }

    void InitEventForSlot(ItemSlotWidget _slot)
    {
        Action _hoverAction = () =>
        {
            itemInformation.gameObject.SetActive(_slot.IsUsed);

            if (_slot.IsUsed)
                itemInformation.Init(_slot.Item);
        };
        _slot.Button.AddHoverAction(_hoverAction, 0.1f);

        _slot.Button.AddOnExitAction(() => itemInformation.gameObject.SetActive(false));
    }

    public void ChangeHealthBar(int _value, int _maxValue)
    {
        healthBar.SetValue(_value, _maxValue);
    }

    public void ChangeRessourceBar(int _value, int _maxValue)
    {
        ressourceBar.SetValue(_value, _maxValue);
    }

    public void ToggleInventory()
    {
        bool _newValue = !inventory.activeInHierarchy;
        inventory.SetActive(_newValue);

        if (_newValue)
            InitInventoryItems();
    }

    void InitInventoryItems()
    {
        List<ItemInventoryData> _items = OnOpenInventory?.Invoke();
        if (_items == null) return;

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

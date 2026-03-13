using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class PlayerOverlayWidget : MonoBehaviour
{
    public event Func<List<ItemInventoryData>> OnOpenInventory;
    public event Action<ItemInventoryData,int> OnMoveItemInInventory;

    [Header("Parameters")]

    /// <summary>
    /// Put to Stats Widgets
    /// </summary>
    [SerializeField] CustomSlider healthBar;
    [SerializeField] CustomSlider ressourceBar;
    [SerializeField] CustomSlider experienceBar;

    [SerializeField] GameObject panel;

    [SerializeField] PlayerStatsWidget statsWidget;
    [SerializeField] PlayerEquipmentWidget equipmentWidget;
    [SerializeField] PlayerInventoryWidget inventoryWidget;

    [SerializeField] ItemInformationWidget informationWidget;

    [SerializeField] Image selectedItemIcon;
    ItemInventoryData? selectedItem;
    bool hasSelectedItem;

    public PlayerInventoryWidget InventoryWidget => inventoryWidget;
    public PlayerEquipmentWidget EquipmentWidget => equipmentWidget;


    private void Awake()
    {
        List<ItemSlotWidget> _allItemSlots = GetComponentsInChildren<ItemSlotWidget>(true).ToList();

        foreach (ItemSlotWidget _slot in _allItemSlots)
        {
            Action _hoverAction = () =>
            {
                informationWidget.gameObject.SetActive(_slot.IsUsed);

                if (_slot.IsUsed)
                    informationWidget.Init(_slot.Item);
            };
            _slot.Button.AddHoverAction(_hoverAction, 0.1f);
            _slot.Button.AddOnExitAction(() => informationWidget.gameObject.SetActive(false));
        }

        equipmentWidget.OnGetSelectedItem += () => selectedItem;
        inventoryWidget.OnSelectWidget += SelectItem;

    }

    private void Update()
    {
        if (hasSelectedItem)
        {
            selectedItemIcon.transform.position = Input.mousePosition;
        }
    }

    public void ChangeHealthBar(int _value, int _maxValue)
    {
        healthBar.SetGoalValue(_value, _maxValue);
    }

    public void ChangeRessourceBar(int _value, int _maxValue)
    {
        ressourceBar.SetGoalValue(_value, _maxValue);
    }

    public void ChangeExperienceBar(int _value, int _maxValue)
    {
        experienceBar.SetGoalValue(_value, _maxValue);
    }

    public void ToggleInventoryPanel()
    {
        bool _newValue = !panel.activeInHierarchy;
        panel.SetActive(_newValue);

        if (_newValue)
        {
            GameManager.Instance.Player.ClickComponent.SetCanClick(false);

            InitInventoryItems();
            statsWidget.RefreshValues();
            equipmentWidget.ClearEquipmentInteractable();
        }
        else
        {
            GameManager.Instance.Player.ClickComponent.SetCanClick(true);

            ResetSelectedItem();
        }
    }

    void ResetSelectedItem()
    {
        selectedItem = null;
        selectedItemIcon.gameObject.SetActive(false);
        hasSelectedItem = false;

        equipmentWidget.ClearEquipmentInteractable();
    }

    void SetSelectedItem(ItemSlotWidget _slot)
    {
        selectedItem = _slot.Item;
        selectedItemIcon.sprite = _slot.Item.data.itemIcon;
        selectedItemIcon.gameObject.SetActive(true);
        hasSelectedItem = true;

        equipmentWidget.ChangeEquipmentInteractable(_slot.Item.data);
    }

    void InitInventoryItems()
    {
        List<ItemInventoryData> _items = OnOpenInventory?.Invoke();
        if (_items == null) return;

        inventoryWidget.Init(_items);
    }

    public void ReinitializeInventory()
    {
        ResetSelectedItem();
        InitInventoryItems();
        statsWidget.RefreshValues();
    }

    public void SelectItem(ItemSlotWidget _slot)
    {
        if (!hasSelectedItem)
        {
            if (_slot == null ||_slot.Item.data == null) return;

            SetSelectedItem(_slot);

            if (_slot.IsUsed)
                _slot.ResetSlot();
        }
        else
        {
            OnMoveItemInInventory?.Invoke(selectedItem.Value,inventoryWidget.GetIndexOfSlot(_slot));
            ReinitializeInventory();
        }
    }
}

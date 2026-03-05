using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class PlayerOverlayWidget : MonoBehaviour
{
    public event Func<List<ItemInventoryData>> OnOpenInventory;

    [Header("Parameters")]
    PlayerStatsWidget statsWidget;

    /// <summary>
    /// Put to Stats Widgets
    /// </summary>
    [SerializeField] CustomSlider healthBar;
    [SerializeField] CustomSlider ressourceBar;
    [SerializeField] CustomSlider experienceBar;

    [SerializeField] GameObject inventory;

    [SerializeField] ItemInformationWidget informationWidget;
    [SerializeField] PlayerEquipmentWidget equipmentWidget;
    [SerializeField] PlayerInventoryWidget inventoryWidget;

    [SerializeField] Image selectedItemIcon;
    Item selectedItem;
    bool hasSelectedItem;

    public PlayerInventoryWidget InventoryWidget => inventoryWidget;


    private void Awake()
    {
        statsWidget = GetComponentInChildren<PlayerStatsWidget>(true);
        equipmentWidget = GetComponentInChildren<PlayerEquipmentWidget>(true);
        informationWidget = GetComponentInChildren<ItemInformationWidget>(true);
        inventoryWidget = GetComponentInChildren<PlayerInventoryWidget>(true);

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
        bool _newValue = !inventory.activeInHierarchy;
        inventory.SetActive(_newValue);

        if (_newValue)
        {
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
    }

    void InitInventoryItems()
    {
        List<ItemInventoryData> _items = OnOpenInventory?.Invoke();
        if (_items == null) return;

        inventoryWidget.Init(_items);
    }

    void SelectItem(ItemSlotWidget _slot)
    {
        selectedItem = _slot.Item.data;
        selectedItemIcon.sprite = _slot.Item.data.itemIcon;
        selectedItemIcon.gameObject.SetActive(true);
        hasSelectedItem = true;

        equipmentWidget.ChangeEquipmentInteractable(_slot.Item.data);
    }
}

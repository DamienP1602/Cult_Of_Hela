using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryOverlayWidget : MonoBehaviour
{
    public event Func<List<ItemInventoryData>> OnOpenInventory;
    public event Action<ItemInventoryData, int> OnMoveItemInInventory;

    [SerializeField] PlayerStatsWidget statsWidget;
    [SerializeField] PlayerEquipmentWidget equipmentWidget;
    [SerializeField] PlayerInventoryWidget inventoryWidget;

    [SerializeField] ItemInformationWidget informationWidget;

    [SerializeField] Image selectedItemIcon;
    ItemInventoryData? selectedItem;
    bool hasSelectedItem;

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
        inventoryWidget.OnAutoEquipItem += AutoEquipItem;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hasSelectedItem)
        {
            selectedItemIcon.transform.position = Input.mousePosition;
        }
    }

    public void InitInventoryOverlay(PlayerEntity _player)
    {
        _player.InventoryComponent.OnAddGold += inventoryWidget.SetGoldText;

        OnOpenInventory += _player.InventoryComponent.GetItems;
        OnMoveItemInInventory += _player.InventoryComponent.MoveItem;

        equipmentWidget.OnItemEquiped += (_item, _slot) =>
        {
            _player.InventoryComponent.RemoveToInventory(_item);
            _player.EquipmentComponent.EquipItem(_item.data, _slot);
            _player.StatsComponent.AddBonuses(_item.data);

            _player.VisualEquipmentComponent.AddMeshOnSlot(_item.data, _slot);

            ReinitializeInventory();
        };

        equipmentWidget.OnItemDesequip += (_item, _slot) =>
        {
            _player.StatsComponent.RemoveBonuses(_item.data);
            _player.EquipmentComponent.DesequipItem(_item.data, _slot);
            _player.InventoryComponent.AddToInventory(_item.data);

            _player.VisualEquipmentComponent.RemoveMeshOnSlot(_slot);

            ReinitializeInventory();
        };

        equipmentWidget.OnSelectWidget += (_item, _slot) =>
        {
            _player.StatsComponent.RemoveBonuses(_item.Item.data);
            _player.EquipmentComponent.DesequipItem(_item.Item.data, _slot);

            _player.VisualEquipmentComponent.RemoveMeshOnSlot(_slot);

            SelectItem(_item);
        };

        equipmentWidget.Init();
        inventoryWidget.Init();
    }

    public void SetInventoryUsage(bool _value)
    {
        if (_value)
        {
            InitInventoryItems();
            statsWidget.RefreshValues();
            equipmentWidget.ClearEquipmentInteractable();
        }
        else
        {
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

        inventoryWidget.InitSlots(_items);
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
            if (_slot == null || _slot.Item.data == null) return;

            SetSelectedItem(_slot);

            if (_slot.IsUsed)
                _slot.ResetSlot();
        }
        else
        {
            OnMoveItemInInventory?.Invoke(selectedItem.Value, inventoryWidget.GetIndexOfSlot(_slot));
            ReinitializeInventory();
        }
    }

    void AutoEquipItem(ItemSlotWidget _slot)
    {
        if (_slot.IsUsed)
        {
            SetSelectedItem(_slot);
            equipmentWidget.AutoEquipItem(_slot);
            ResetSelectedItem();
        }
    }
}

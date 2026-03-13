using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct ItemInventoryData
{
    public Item data;
    public int inventoryPosition;

    public ItemInventoryData(Item _item, int _position)
    {
        data = _item;
        inventoryPosition = _position;
    }

    public override bool Equals(object obj)
    {
        return obj is ItemInventoryData data &&
               EqualityComparer<Item>.Default.Equals(this.data, data.data) &&
               inventoryPosition == data.inventoryPosition;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(data, inventoryPosition);
    }

    public static bool operator ==(ItemInventoryData _self, ItemInventoryData _other)
    {
        return _self.data == _other.data && _self.inventoryPosition == _other.inventoryPosition;
    }

    public static bool operator !=(ItemInventoryData _self, ItemInventoryData _other)
    {
        return _self.data != _other.data || _self.inventoryPosition != _other.inventoryPosition;
    }

    
}

public class PlayerInventoryComponent : MonoBehaviour
{
    public event Action<int> OnAddGold;

    [Header("Parameters")]
    [SerializeField] int coinAmount;
    [SerializeField] List<ItemInventoryData> items = new List<ItemInventoryData>();

    public List<ItemInventoryData> GetItems() => items;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddToInventory(PickUpInteractable _pickUp)
    {
        if (_pickUp.Type == PickUpType.PickUpCoin)
        {
            coinAmount += _pickUp.Amount;
            OnAddGold?.Invoke(coinAmount);
        }
        else if (_pickUp.Type == PickUpType.PickUpItem)
        {
            int _inventoryPos = GetFirstInventoryPositionAvailable();
            ItemInventoryData _newItem = new ItemInventoryData(_pickUp.ItemData, _inventoryPos);
            items.Add(_newItem);
        }
    }

    public void AddToInventory(Item _item)
    {
        items.Add(new ItemInventoryData(_item, GetFirstInventoryPositionAvailable()));
    }

    int GetFirstInventoryPositionAvailable()
    {
        if (items == null)
            return 0;

        int _inventoryPos = 0;

        List<ItemInventoryData> _temp = new List<ItemInventoryData>(items);

        for (int _i = 0; _i < _temp.Count; _i++)
        {
            ItemInventoryData _data = _temp[_i];

            if (_inventoryPos == _data.inventoryPosition)
            {
                _i = -1;
                _temp.Remove(_data);
                _inventoryPos++;
            }
        }

        return _inventoryPos;
    }

    public void RemoveToInventory(ItemInventoryData _data)
    {
        items.Remove(_data);
    }

    public void MoveItem(ItemInventoryData _itemToMove, int _itemPosition)
    {
        ItemInventoryData _tempItem = GetItemFromIndex(_itemPosition);
        if (_tempItem == _itemToMove) return;

        items.Remove(_itemToMove);
        if (_tempItem.data != null)
        {
            items.Remove(_tempItem);
            items.Add(new ItemInventoryData(_tempItem.data, _itemToMove.inventoryPosition));

            items.Add(new ItemInventoryData(_itemToMove.data, _tempItem.inventoryPosition));
        }
        else
        {
            items.Add(new ItemInventoryData(_itemToMove.data, _itemPosition));
        }

    }

    ItemInventoryData GetItemFromIndex(int _index) => items.Find(_item => _item.inventoryPosition == _index);
}

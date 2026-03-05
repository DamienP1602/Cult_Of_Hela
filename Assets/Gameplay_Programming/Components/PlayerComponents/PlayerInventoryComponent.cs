using System;
using System.Collections.Generic;
using UnityEngine;

public struct ItemInventoryData
{
    public Item data;
    public int inventoryPosition;

    public ItemInventoryData(Item _item, int _position)
    {
        data = _item;
        inventoryPosition = _position;
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

}

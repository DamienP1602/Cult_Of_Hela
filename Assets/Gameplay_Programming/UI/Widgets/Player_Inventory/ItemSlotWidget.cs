using UnityEngine;
using UnityEngine.UI;

public class ItemSlotWidget : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] CustomButton button;
    [SerializeField] Image itemIcon;
    [SerializeField] ItemInventoryData item;
    [SerializeField] bool isUsed;
    [SerializeField] bool isClosed;

    public CustomButton Button => button;
    public bool IsUsed => isUsed;
    public ItemInventoryData Item => item;

    public void InitSlot(ItemInventoryData _data)
    {
        itemIcon.sprite = _data.data.itemIcon;
        itemIcon.color = Color.white;
        isUsed = true;
        item = _data;
    }

    public void ResetSlot()
    {
        itemIcon.sprite = null;
        itemIcon.color = Color.clear;
        isUsed = false;
        item = new ItemInventoryData();
    }

    public void SetCloseValue(bool _isClosed)
    {
        isClosed = _isClosed;
    }

    public void SetButtonInteractable(bool _value)
    {
        if (isClosed)
            button.SetInteractable(false);
        else
            button.SetInteractable(_value);
    }
}

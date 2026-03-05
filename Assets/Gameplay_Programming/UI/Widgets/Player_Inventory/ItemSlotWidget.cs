using UnityEngine;
using UnityEngine.UI;

public class ItemSlotWidget : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] CustomButton button;
    [SerializeField] Image itemIcon;
    [SerializeField] ItemInventoryData? item;

    [SerializeField] bool isUsed;

    public CustomButton Button => button;
    public bool IsUsed => isUsed;
    public ItemInventoryData Item => item.Value;
    public bool HasItem => item != null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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
        item = null;
    }
}

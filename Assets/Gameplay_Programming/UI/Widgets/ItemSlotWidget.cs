using UnityEngine;
using UnityEngine.UI;

public class ItemSlotWidget : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] Image itemIcon;

    [SerializeField] bool isUsed;

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
        itemIcon.sprite = _data.item.itemIcon;
        isUsed = true;
    }
}

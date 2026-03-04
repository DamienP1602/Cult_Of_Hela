using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlotWidget : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] CustomButton button;
    [SerializeField] Image itemIcon;
    [SerializeField] Item item;

    [SerializeField] bool isUsed;

    public CustomButton Button => button;
    public bool IsUsed => isUsed;
    public Item Item => item;

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
        item = _data.item;
    }

    public void ResetSlot()
    {
        itemIcon.sprite = null;
        isUsed = false;
        item = null;
    }
}

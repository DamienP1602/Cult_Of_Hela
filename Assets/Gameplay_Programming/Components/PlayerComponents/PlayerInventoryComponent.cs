using UnityEngine;

public class PlayerInventoryComponent : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] int coinAmount;

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
        }
    }
}

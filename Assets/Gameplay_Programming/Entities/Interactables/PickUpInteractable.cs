using UnityEngine;

public enum PickUpType
{
    PickUpCoin
}

public class PickUpInteractable : InteractableEntity
{
    [Header("Parameters")]
    [SerializeField] PickUpType type;
    [SerializeField] int amount;

    public PickUpType Type => type;
    public int Amount
    {
        get
        {
            return amount;
        }
        set
        {
            amount = value;
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    void Update()
    {

    }

    public override void OnInteraction(PlayerEntity _player)
    {
        base.OnInteraction(_player);

        _player.InventoryComponent.AddToInventory(this);
    }
}

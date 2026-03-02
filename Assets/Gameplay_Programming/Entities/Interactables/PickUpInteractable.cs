using UnityEngine;

public enum PickUpType
{
    PickUpCoin,
    PickUpItem
}

public class PickUpInteractable : InteractableEntity
{
    [Header("Parameters")]
    [SerializeField] PickUpType type;
    [SerializeField] int amount;
    [SerializeField] Item itemData;

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
    public Item ItemData => itemData;

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

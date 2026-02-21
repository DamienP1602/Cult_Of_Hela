using UnityEngine;

[RequireComponent(typeof(PlayerInputComponent), typeof(PlayerClickComponent),typeof(PlayerInventoryComponent))]
public class PlayerEntity : BaseEntity
{
    public PlayerInputComponent InputComponent { get; private set; }
    public PlayerClickComponent ClickComponent { get; private set; }
    public PlayerInventoryComponent InventoryComponent { get; private set; }

    protected override void Start()
    {
        base.Start();
    }

    protected override void EventAssignation()
    {
        base.EventAssignation();

        InputComponent.LeftClick.started += (_context) => ClickComponent.SetIsClick(true);
        InputComponent.LeftClick.canceled += (_context) => ClickComponent.SetIsClick(false);
    }

    protected override void Init()
    {
        base.Init();

        InputComponent = GetComponent<PlayerInputComponent>();
        ClickComponent = GetComponent<PlayerClickComponent>();
        InventoryComponent = GetComponent<PlayerInventoryComponent>();
    }
}

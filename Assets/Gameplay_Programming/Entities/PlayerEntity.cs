using UnityEngine;

[RequireComponent(typeof(PlayerInputComponent), typeof(PlayerClickComponent),typeof(PlayerInventoryComponent))]
[RequireComponent(typeof(PlayerCameraComponent))]
public class PlayerEntity : BaseEntity
{
    public PlayerInputComponent InputComponent { get; private set; }
    public PlayerClickComponent ClickComponent { get; private set; }
    public PlayerInventoryComponent InventoryComponent { get; private set; }
    public PlayerCameraComponent CameraComponent { get; private set; }

    protected override void Start()
    {
        base.Start();
    }

    protected override void EventAssignation()
    {
        base.EventAssignation();

        InputComponent.LeftClick.started += (_context) => ClickComponent.SetIsClick(true);
        InputComponent.LeftClick.canceled += (_context) => ClickComponent.SetIsClick(false);

        InputComponent.FirstSpellBinding.started += (_context) => SpellBookComponent.LaunchAbility(0);

        UIEvent();
    }

    void UIEvent()
    {
        StatsComponent.health.onValueChange +=  GameManager.Instance.Hud.Overlay.ChangeHealthBar;
    }

    protected override void Init()
    {
        base.Init();

        InputComponent = GetComponent<PlayerInputComponent>();
        ClickComponent = GetComponent<PlayerClickComponent>();
        InventoryComponent = GetComponent<PlayerInventoryComponent>();
        CameraComponent = GetComponent<PlayerCameraComponent>();
    }
}

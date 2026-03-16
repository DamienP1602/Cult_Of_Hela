using UnityEngine;

[RequireComponent(typeof(PlayerInputComponent), typeof(PlayerClickComponent), typeof(PlayerInventoryComponent))]
[RequireComponent(typeof(PlayerCameraComponent), typeof(PlayerLevelComponent), typeof(PlayerVisualEquipmentComponent))]
[RequireComponent(typeof(PlayerEquipmentComponent))]
public class PlayerEntity : BaseEntity
{
    public PlayerInputComponent InputComponent { get; private set; }
    public PlayerClickComponent ClickComponent { get; private set; }
    public PlayerInventoryComponent InventoryComponent { get; private set; }
    public PlayerCameraComponent CameraComponent { get; private set; }
    public PlayerLevelComponent LevelComponent { get; private set; }
    public PlayerVisualEquipmentComponent VisualEquipmentComponent { get; private set; }
    public PlayerEquipmentComponent EquipmentComponent { get; private set; }

    protected override void Start()
    {
        base.Start();
    }

    protected override void EventAssignation()
    {
        base.EventAssignation();

        InputComponent.LeftClick.started += (_context) => ClickComponent.SetIsClick(true);
        InputComponent.LeftClick.started += (_context) => ClickComponent.SpawnClickVFX();

        InputComponent.LeftClick.canceled += (_context) => ClickComponent.SetIsClick(false);

        InputComponent.Inventory.started += (_context) => GameManager.Instance.Hud.Overlay.InventoryOverlay.ToggleInventory();

        InputComponent.FirstSpellBinding.started += (_context) => SpellBookComponent.LaunchAbility(0);
        InputComponent.SecondSpellBinding.started += (_context) => SpellBookComponent.LaunchAbility(1);
    }

    protected override void Init()
    {
        base.Init();

        InputComponent = GetComponent<PlayerInputComponent>();
        ClickComponent = GetComponent<PlayerClickComponent>();
        InventoryComponent = GetComponent<PlayerInventoryComponent>();
        CameraComponent = GetComponent<PlayerCameraComponent>();
        LevelComponent = GetComponent<PlayerLevelComponent>();
        VisualEquipmentComponent = GetComponent<PlayerVisualEquipmentComponent>();
        EquipmentComponent = GetComponent<PlayerEquipmentComponent>();

        InitUI();
    }

    void InitUI()
    {
        GameManager.Instance.Hud.Overlay.InitPlayerOverlay(this);
    }
}

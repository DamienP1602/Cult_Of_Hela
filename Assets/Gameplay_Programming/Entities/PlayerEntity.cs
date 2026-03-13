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

        InputComponent.Inventory.started += (_context) => GameManager.Instance.Hud.Overlay.ToggleInventoryPanel();

        InputComponent.FirstSpellBinding.started += (_context) => SpellBookComponent.LaunchAbility(0);
        InputComponent.SecondSpellBinding.started += (_context) => SpellBookComponent.LaunchAbility(1);

        UIEvent();
    }

    void UIEvent()
    {
        StatsComponent.health.onValueChange += GameManager.Instance.Hud.Overlay.ChangeHealthBar;
        StatsComponent.ressource.onValueChange += GameManager.Instance.Hud.Overlay.ChangeRessourceBar;
        LevelComponent.OnGainExperience += GameManager.Instance.Hud.Overlay.ChangeExperienceBar;

        InventoryComponent.OnAddGold += GameManager.Instance.Hud.Overlay.InventoryWidget.SetGoldText;

        GameManager.Instance.Hud.Overlay.OnOpenInventory += InventoryComponent.GetItems;
        GameManager.Instance.Hud.Overlay.OnMoveItemInInventory += InventoryComponent.MoveItem;

        GameManager.Instance.Hud.Overlay.EquipmentWidget.OnItemEquiped += (_item, _slot) =>
        {
            Item _returnedItem = EquipmentComponent.EquipItem(_item.data, _slot);

            InventoryComponent.RemoveToInventory(_item);
            StatsComponent.AddBonuses(_item.data);

            if (_returnedItem)
            {
                StatsComponent.RemoveBonuses(_returnedItem);
                InventoryComponent.AddToInventory(_returnedItem);
            }

            GameManager.Instance.Hud.Overlay.ReinitializeInventory();
        };
        GameManager.Instance.Hud.Overlay.EquipmentWidget.OnSelectWidget += (_item, _slot) =>
        {
            StatsComponent.RemoveBonuses(_item.Item.data);
            EquipmentComponent.DesequipItem(_item.Item.data, _slot);

            GameManager.Instance.Hud.Overlay.SelectItem(_item);
        };
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
    }
}

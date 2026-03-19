using UnityEngine;

public class PlayerMenuOverlay : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] PlayerInventoryOverlayWidget inventoryOverlayWidget;
    [SerializeField] PlayerAbilitiesWidget playerAbilitiesWidget;
    [SerializeField] MenuSelectorWidget menuSelectorWidget;

    public PlayerInventoryOverlayWidget InventoryOverlayWidget => inventoryOverlayWidget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitMenuOverlay(PlayerEntity _player)
    {
        SwitchButton(menuSelectorWidget.InventoryButton, menuSelectorWidget.AbilitiesButton);
        inventoryOverlayWidget.InitInventoryOverlay(_player);

        menuSelectorWidget.AbilitiesButton.AddLeftClickAction(() =>
        {
            playerAbilitiesWidget.gameObject.SetActive(true);
            inventoryOverlayWidget.gameObject.SetActive(false);

            SwitchButton(menuSelectorWidget.AbilitiesButton, menuSelectorWidget.InventoryButton);
        });

        menuSelectorWidget.InventoryButton.AddLeftClickAction(() =>
        {
            playerAbilitiesWidget.gameObject.SetActive(false);
            inventoryOverlayWidget.gameObject.SetActive(true);

            SwitchButton(menuSelectorWidget.InventoryButton, menuSelectorWidget.AbilitiesButton);
        });
    }

    void SwitchButton(CustomButton _buttonToDesactivate, CustomButton _buttonToActivate)
    {
        _buttonToDesactivate.SetInteractable(false);
        _buttonToDesactivate.ButtonText.color = Color.white;

        _buttonToActivate.SetInteractable(true);
        _buttonToActivate.ButtonText.color = Color.gray;
    }
}

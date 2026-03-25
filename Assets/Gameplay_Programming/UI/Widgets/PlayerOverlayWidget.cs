using TMPro;
using UnityEngine;

public class PlayerOverlayWidget : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] PlayerGameOverlayWidget gameOverlayWidget;
    [SerializeField] PlayerMenuOverlay menuWidget;

    public PlayerGameOverlayWidget GameOverlay => gameOverlayWidget;
    public PlayerMenuOverlay Menu => menuWidget;

    private void Awake()
    {

    }

    private void Update()
    {

    }

    public void InitPlayerOverlay(PlayerEntity _player)
    {
        gameOverlayWidget.InitGameOverlay(_player);
        menuWidget.InitMenuOverlay(_player);
    }

    public void ToggleInventoryOverlay()
    {
        bool _newValue = !menuWidget.InventoryOverlayWidget.isActiveAndEnabled;

        if (_newValue)
            menuWidget.ShowInventory();
        else
            menuWidget.gameObject.SetActive(false);

        GameManager.Instance.Player.ClickComponent.SetIsInUI(_newValue);
        menuWidget.InventoryOverlayWidget.SetInventoryUsage(_newValue);

        menuWidget.gameObject.SetActive(_newValue);

    }

    public void ToggleAbilitiesOverlay()
    {
        bool _newValue = !menuWidget.PlayerAbilitiesWidget.isActiveAndEnabled;

        if (_newValue)
        {
            menuWidget.ShowAbilities();
        }
        else
            menuWidget.gameObject.SetActive(false);

        GameManager.Instance.Player.ClickComponent.SetIsInUI(_newValue);
        menuWidget.gameObject.SetActive(_newValue);
    }
}

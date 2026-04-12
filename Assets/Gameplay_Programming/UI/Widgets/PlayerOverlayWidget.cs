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

        PlayerEntity _player = GameManager.Instance.Player;
        _player.ClickComponent.SetIsInUI(_newValue);

        _player.SpellBookComponent.CanLaunchSpell = !_newValue;

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
            menuWidget.PlayerAbilitiesWidget.HideAbilitiesPanel();

        PlayerEntity _player = GameManager.Instance.Player;
        _player.ClickComponent.SetIsInUI(_newValue);

        _player.SpellBookComponent.CanLaunchSpell = !_newValue;

        menuWidget.gameObject.SetActive(_newValue);
    }
}

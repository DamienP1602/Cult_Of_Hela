using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerAccessWidget : MonoBehaviour
{
    [SerializeField] List<SpellButtonWidget> buttons;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(PlayerEntity _player)
    {
        foreach (SpellButtonWidget _widget in buttons)
        {
            _widget.Button.AddOnEnterAction(() => _player.ClickComponent.SetCanClick(false));
            _widget.Button.AddOnExitAction(() => _player.ClickComponent.SetCanClick(true));
        }

        // Open Inventory
        buttons[0].Button.AddLeftClickAction(() => GameManager.Instance.Hud.Overlay.ToggleInventoryOverlay());
    }
}

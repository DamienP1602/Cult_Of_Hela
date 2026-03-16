using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerOverlayWidget : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] PlayerGameOverlayWidget gameOverlayWidget;
    [SerializeField] PlayerInventoryOverlayWidget inventoryOverlayWidget;

    public PlayerGameOverlayWidget GameOverlay => gameOverlayWidget;
    public PlayerInventoryOverlayWidget InventoryOverlay => inventoryOverlayWidget;

    private void Awake()
    {

    }

    private void Update()
    {

    }  

    public void InitPlayerOverlay(PlayerEntity _player)
    {
        gameOverlayWidget.InitGameOverlay(_player);
        inventoryOverlayWidget.InitInventoryOverlay(_player);
    }
}

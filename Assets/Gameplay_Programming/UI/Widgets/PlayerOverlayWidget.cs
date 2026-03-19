using TMPro;
using UnityEngine;
using System;
using System.Collections.Generic;

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

    public void ToggleMenuOverlay()
    {
        bool _newValue = !menuWidget.isActiveAndEnabled;
        menuWidget.gameObject.SetActive(_newValue);

        menuWidget.InventoryOverlayWidget.SetInventoryUsage(_newValue);
    }
}

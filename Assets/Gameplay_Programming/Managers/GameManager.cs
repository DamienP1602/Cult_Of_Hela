using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class GameManager : Singleton<GameManager>
{
    HUD hud;
    PlayerEntity player;

    [SerializeField] VisualEffect emptyVisualEffect;
    [SerializeField] VisualEquipmentComponent emptyItemMesh;

    public PlayerEntity Player => player;
    public HUD Hud => hud;

    public VisualEffect EmptyVisualEffect => emptyVisualEffect;
    public VisualEquipmentComponent EmptyItemMesh => emptyItemMesh;

    protected override void Awake()
    {
        base.Awake();

        hud = GetComponent<HUD>();
    }

    void Start()
    {
        player = FindAnyObjectByType<PlayerEntity>();
    }

}

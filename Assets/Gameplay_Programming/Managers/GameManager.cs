using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    HUD hud;
    PlayerEntity player;

    public PlayerEntity Player => player;
    public HUD Hud => hud;

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

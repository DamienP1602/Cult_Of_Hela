using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct SpawnableObjects
{
    public PickUpType type;
    public PickUpInteractable entity;
}

public class GameManager : Singleton<GameManager>
{
    [Header("Parameters")]
    [SerializeField] List<SpawnableObjects> spawnables;

    HUD hud;
    PlayerEntity player;

    public PlayerEntity Player => player;
    public HUD Hud => hud;

    public List<SpawnableObjects> Spawnables => spawnables;

    protected override void Awake()
    {
        base.Awake();

        hud = GetComponent<HUD>();
    }

    void Start()
    {
        player = FindAnyObjectByType<PlayerEntity>();
    }

    public PickUpInteractable GetSpawnableByType(PickUpType _type)
    {
        foreach (SpawnableObjects _spawnable in spawnables)
        {
            if (_spawnable.type == _type)
                return _spawnable.entity;
        }

        return null;
    }
}

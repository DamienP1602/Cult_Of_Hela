using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct LootObject
{
    [Header("Object Parameters")]
    public PickUpInteractable objectToDrop;
    public Vector2 dropAmount;

    [Header("Drop Parameters")]
    public float dropChance;
}

[CreateAssetMenu(fileName = "New LootTable", menuName = "Scriptable Objects/Loots/LootTable")]
public class LootTable : ScriptableObject
{
    public List<LootObject> allDroppedObjects;
}

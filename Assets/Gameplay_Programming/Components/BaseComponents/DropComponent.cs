using System.Collections.Generic;
using UnityEngine;

public class DropComponent : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] LootTable lootTable;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DropLootTable()
    {
        if (!lootTable) return;

        float _spawnOffset = 1.0f;
        foreach (LootObject _drop in lootTable.allDroppedObjects)
        {
            float _dropChance = Random.Range(0.0f, 100.0f);
            if (_dropChance > _drop.dropChance) continue;

            Vector3 _spawnPosition = GetSpawnPosition(_spawnOffset);
            CreateObject(_drop, _spawnPosition);

            _spawnOffset *= 1.33f;
        }
    }

    void CreateObject(LootObject _drop, Vector3 _randomPos)
    {
        int _randomAmount = Random.Range((int)_drop.dropAmount.x, (int)_drop.dropAmount.y + 1);

        PickUpInteractable _object = Instantiate(_drop.objectToDrop, _randomPos + transform.position, Quaternion.identity);
        _object.Amount = _randomAmount;
    }

    Vector3 GetSpawnPosition(float _spawnOffset)
    {
        float _timeUsed = Time.time * _spawnOffset;

        float _x = Mathf.Sin(_timeUsed);
        float _z = Mathf.Cos(_timeUsed);
        Vector3 _randomPos = new Vector3(_x, 0.0f, _z);

        float _value = Vector3.Dot(transform.forward, _randomPos);
        if (_value <= 0.2f || _value >= 0.8f)
            _randomPos.Set(Mathf.Abs(_x), 0.0f, Mathf.Abs(_z));

        return _randomPos;
    }
}

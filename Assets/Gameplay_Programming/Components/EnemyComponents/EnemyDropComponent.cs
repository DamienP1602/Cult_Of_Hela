using UnityEngine;

public class EnemyDropComponent : MonoBehaviour
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
        foreach (LootObject _drop in lootTable.allDroppedObjects)
        {
            float _dropChance = Random.Range(0.0f, 100.0f);
            if (_dropChance > _drop.dropChance) continue;

            Vector3 _randomPos = new Vector3(Random.Range(-1.0f, 1.0f), 0.0f, Random.Range(-1.0f, 1.0f));
            int _randomAmount = Random.Range((int)_drop.dropAmount.x, (int)_drop.dropAmount.y);

            PickUpInteractable _object = Instantiate(GameManager.Instance.GetSpawnableByType(_drop.objectToDrop.Type), _randomPos + transform.position, Quaternion.identity);
            _object.Amount = _randomAmount;
        }
    }
}

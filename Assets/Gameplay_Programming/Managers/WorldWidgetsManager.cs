using System.Collections.Generic;
using UnityEngine;

public class WorldWidgetsManager : Singleton<WorldWidgetsManager>
{
    [SerializeField] DamageTextWorldWidget damageTextWorldWidget;

    List<DamageTextWorldWidget> worldWidgetsSpawned = new List<DamageTextWorldWidget>();

    int AllWidgetsCount => worldWidgetsSpawned.Count;

    void Update()
    {
        for (int _i = 0; _i < AllWidgetsCount; _i++)
        {
            DamageTextWorldWidget _widget = worldWidgetsSpawned[_i];

            _widget.Group.alpha -= Time.deltaTime;
            if (_widget.Group.alpha <= 0.0f)
            {
                Destroy(_widget.gameObject);

                worldWidgetsSpawned.Remove(_widget);
                _i--;
            }
        }
    }

    public void SpawnDamageText(Vector3 _pos, int _damage)
    {
        DamageTextWorldWidget _text = Instantiate(damageTextWorldWidget, _pos, Quaternion.identity);
        _text.InitText(_damage);
        worldWidgetsSpawned.Add(_text);
    }
}

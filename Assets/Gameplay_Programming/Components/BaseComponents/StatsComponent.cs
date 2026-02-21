using System;
using UnityEngine;

[Serializable]
public class StatValue
{
    [SerializeField] int value;
    public int Value => value;

    public StatValue(int _value) => value = _value;

    public void AddValue(int _value) => value += _value;
    public void RemoveValue(int _value) => value -= _value;
    public void SetValue(int _value) => value = _value;
}

public class StatsComponent : MonoBehaviour
{
    public event Action onDeath;

    [Header("Parameters")]
    [SerializeField] StatValue currentHealth;
    public StatValue damage;

    public void LooseHealth(int _damage)
    {
        currentHealth.RemoveValue(_damage);

        if (currentHealth.Value <= 0)
        {
            onDeath?.Invoke();
        }
    }

}

using System;
using UnityEngine;

[Serializable]
public class SingleStat
{
    public event Action<int> onValueChange;

    [SerializeField] int value;
    public int Value => value;

    public SingleStat(int _value) => value = _value;

    public void AddValue(int _value)
    {
        value += _value;
        onValueChange?.Invoke(value);
    }

    public void RemoveValue(int _value)
    {
        value -= _value;
        onValueChange?.Invoke(value);
    }

    public void SetValue(int _value)
    {
        value = _value;
        onValueChange?.Invoke(value);
    }
}

[Serializable]
public class MultipleStat
{
    public event Action<int,int> onValueChange;

    [SerializeField] int value;
    [SerializeField] int maxValue;
    
    public int Value => value;
    public int MaxValue => maxValue;

    public MultipleStat(int _value,int _maxValue)
    {
        value = _value;
        maxValue = _maxValue;
    }

    public void AddValue(int _value)
    {
        value += _value;
        value = Mathf.Clamp(value, 0, maxValue);

        onValueChange?.Invoke(value,maxValue);
    }

    public void RemoveValue(int _value)
    {
        value -= _value;
        value = Mathf.Clamp(value, 0, maxValue);

        onValueChange?.Invoke(value, maxValue);
    }

    public void SetValue(int _value)
    {
        value = _value;
        onValueChange?.Invoke(value, maxValue);
    }

    public void AddMaxValue(int _value)
    {
        value += _value;
        value = Mathf.Clamp(value, 0, maxValue);

        onValueChange?.Invoke(value, maxValue);
    }

    public void RemoveMaxValue(int _value)
    {
        value -= _value;
        value = Mathf.Clamp(value, 0, maxValue);

        onValueChange?.Invoke(value, maxValue);
    }

    public void SetMaxValue(int _value)
    {
        value = _value;
        onValueChange?.Invoke(value, maxValue);
    }
}

public class StatsComponent : MonoBehaviour
{
    public event Action onDeath;

    [Header("Parameters")]
    public MultipleStat health;
    public MultipleStat mana;
    public SingleStat damage;

    public void LooseHealth(int _damage)
    {
        health.RemoveValue(_damage);

        if (health.Value <= 0)
        {
            onDeath?.Invoke();
        }
    }

}

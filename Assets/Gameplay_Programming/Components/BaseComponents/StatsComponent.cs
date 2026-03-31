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
    public event Action<int, int> onValueChange;

    [SerializeField] int value;
    [SerializeField] int maxValue;

    public int Value => value;
    public int MaxValue => maxValue;

    public MultipleStat(int _value, int _maxValue)
    {
        value = _value;
        maxValue = _maxValue;
    }

    public void AddValue(int _value)
    {
        value += _value;
        value = Mathf.Clamp(value, 0, maxValue);

        onValueChange?.Invoke(value, maxValue);
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
        maxValue += _value;

        onValueChange?.Invoke(value, maxValue);
    }

    public void RemoveMaxValue(int _value)
    {
        maxValue -= _value;

        onValueChange?.Invoke(value, maxValue);
    }

    public void SetMaxValue(int _value)
    {
        maxValue = _value;
        onValueChange?.Invoke(value, maxValue);
    }
}

public class StatsComponent : MonoBehaviour
{
    public event Action onDeath;

    [Header("Multiple Stats")]
    public MultipleStat health;
    public MultipleStat ressource;
    public MultipleStat damages;

    [Header("Unique Stats")]
    public SingleStat strength;
    public SingleStat dexterity;
    public SingleStat intelligence;
    public SingleStat vitality;
    public SingleStat spirit;
    public SingleStat armor;

    [Header("Bonus Stats")]
    [SerializeField] bool canRegenerate;
    public SingleStat healthRegeneration;
    public SingleStat ressourceRegeneration;
    float currentTimeRegeneration;

    [Header("Parameters")]
    [SerializeField] CustomEffectInterface<BonusStatsEffect> bonusEffects = new CustomEffectInterface<BonusStatsEffect>();

    public bool IsDead => health.Value <= 0;
    public int BonusAttack => strength.Value / 2;
    public int BonusSpell => intelligence.Value / 2;

    public CustomEffectInterface<BonusStatsEffect> StatsBonuses => bonusEffects;

    private void Update()
    {
        bonusEffects.UpdateCustomEffect(RemoveBonuses);

        if (canRegenerate)
            RegenerationUpdate();
    }

    void RegenerationUpdate()
    {
        currentTimeRegeneration += Time.deltaTime;
        if (currentTimeRegeneration >= 3.0f)
        {
            PassifRegeneration();
            currentTimeRegeneration = 0.0f;
        }
    }

    void PassifRegeneration()
    {
        float _healthValue = health.Value / (healthRegeneration.Value * 100.0f);
        _healthValue = Mathf.Clamp(_healthValue, 1.0f, float.MaxValue);
        health.AddValue((int)_healthValue);

        float _ressourceValue = ressource.Value / (ressourceRegeneration.Value * 100.0f);
        _ressourceValue = Mathf.Clamp(_ressourceValue, 1.0f, float.MaxValue);
        ressource.AddValue((int)_ressourceValue);
    }

    /// <summary>
    /// Return true if this entity is dead
    /// </summary>
    /// <param name="_damage"></param>
    /// <returns></returns>
    public bool LooseHealth(int _damage)
    {
        float _percentReduction = (armor.Value / 10.0f) / 100.0f;
        _percentReduction = Math.Clamp(_percentReduction, 0.0f, 0.75f);

        int _newDamageAmount = (int)(_damage * _percentReduction);
        health.RemoveValue(_damage - _newDamageAmount);

        if (GetComponent<EnemyEntity>())
            WorldWidgetsManager.Instance.SpawnDamageText(transform.position + (Vector3.up * 2.0f), _damage);

        if (health.Value <= 0)
        {
            onDeath?.Invoke();
            return true;
        }

        return false;
    }

    public int GetDamageDeal()
    {
        int _random = UnityEngine.Random.Range(damages.Value, damages.MaxValue + 1);

        _random += BonusAttack;

        return _random;
    }

    public void AddBonuses(Item _item)
    {
        damages.AddMaxValue((int)_item.damages.y);
        damages.AddValue((int)_item.damages.x);

        strength.AddValue(_item.strength);
        intelligence.AddValue(_item.intelligence);
        dexterity.AddValue(_item.dexterity);
        vitality.AddValue(_item.vitality);
        spirit.AddValue(_item.spirit);

        armor.AddValue(_item.armor);

        CalculMaxStats();
    }

    public void AddBonuses(BonusStatsEffect _effect)
    {
        int _bonusValue = (int)_effect.ActivateEffect(this, GetComponent<PlayerLevelComponent>());

        switch (_effect.statToBoost)
        {
            case StatBonus.BonusStrength:
                strength.AddValue(_bonusValue);
                break;
        }

        bonusEffects.AddEffect(_effect);

        CalculMaxStats();
    }

    public void RemoveBonuses(Item _item)
    {
        damages.RemoveMaxValue((int)_item.damages.y);
        damages.RemoveValue((int)_item.damages.x);

        strength.RemoveValue(_item.strength);
        intelligence.RemoveValue(_item.intelligence);
        dexterity.RemoveValue(_item.dexterity);
        vitality.RemoveValue(_item.vitality);
        spirit.RemoveValue(_item.spirit);

        armor.RemoveValue(_item.armor);

        CalculMaxStats();
    }

    public void RemoveBonuses(BonusStatsEffect _effect)
    {
        int _bonusValue = (int)_effect.ActivateEffect(this, GetComponent<PlayerLevelComponent>());

        switch (_effect.statToBoost)
        {
            case StatBonus.BonusStrength:
                strength.RemoveValue(_bonusValue);
                break;
        }

        CalculMaxStats();
    }

    public void InitStats()
    {
        health.SetMaxValue(10 + vitality.Value * 3);
        health.SetValue(10 + vitality.Value * 3);

        ressource.SetMaxValue(10 + spirit.Value * 2);
        ressource.SetValue(10 + spirit.Value * 2);
    }

    public void CalculMaxStats()
    {
        health.SetMaxValue(10 + vitality.Value * 3);
        health.SetValue(health.Value);

        ressource.SetMaxValue(10 + spirit.Value * 2);
        ressource.SetValue(ressource.Value);
    }

    public void LevelUpStats()
    {
        strength.AddValue(1);
        intelligence.AddValue(1);
        dexterity.AddValue(1);
        vitality.AddValue(1);
        spirit.AddValue(1);

        InitStats();
    }
}

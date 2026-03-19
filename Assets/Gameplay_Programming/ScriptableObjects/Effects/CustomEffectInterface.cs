using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CustomEffectData<T>
{
    public T effect;
    public float currentDuration;

    public CustomEffectData(T _effect)
    {
        effect = _effect;
        currentDuration = 0.0f;
    }
}

[Serializable]
public class CustomEffectInterface<T> where T : CustomEffect
{
    [SerializeField] List<CustomEffectData<T>> allEffect = new List<CustomEffectData<T>>();

    public List<CustomEffectData<T>> EffectList => allEffect;
    public int Count => allEffect.Count;

    /// <summary>
    /// Add Effect in list and if effect is unique, won't add it if there's another copy of it
    /// </summary>
    /// <param name="_effect"></param>
    public void AddEffect(T _effect)
    {
        if (_effect.uniqueEffect)
        {
            foreach (CustomEffectData<T> _data in allEffect)
            {
                if (_data.effect.effectID == _effect.effectID)
                    return;
            }
        }

        allEffect.Add(new CustomEffectData<T>(_effect));
    }

    /// <summary>
    /// Remove effect in list
    /// </summary>
    /// <param name="_data"></param>
    public void RemoveEffect(string _effectID)
    {
        foreach (CustomEffectData<T> _data in allEffect)
        {
            if (_data.effect.effectID == _effectID)
            {
                allEffect.Remove(_data);
                return;
            }
        }
    }

    /// <summary>
    /// return true if there's another effect in list with the same ID
    /// </summary>
    /// <param name="_effectID"></param>
    /// <returns></returns>
    public bool HasEffect(string _effectID)
    {
        foreach (CustomEffectData<T> _data in allEffect)
        {
            if (_data.effect.effectID == _effectID)
                return true;
        }
        return false;
    }

    /// <summary>
    /// Return the effect from an index, check for safety (< 0 or > size)
    /// </summary>
    /// <param name="_index"></param>
    /// <returns></returns>
    public CustomEffectData<T> GetEffect(int _index)
    {
        if (_index < 0 || _index >= allEffect.Count)
            return null;

        return allEffect[_index];
    }

    /// <summary>
    /// Update list, will remove effect if the time limit is reached
    /// </summary>
    public void UpdateCustomEffect(Action<T> _effectOnDestroy = null)
    {
        for (int _i = 0; _i < EffectList.Count; _i++)
        {
            CustomEffectData<T> _data = EffectList[_i];
            if (_data.effect.hasDuration)
            {
                _data.currentDuration += Time.deltaTime;
                if (_data.currentDuration >= _data.effect.duration)
                {
                    RemoveEffect(_data.effect.effectID);
                    _effectOnDestroy?.Invoke(_data.effect);
                    _i--;
                }
            }
        }
    }
}

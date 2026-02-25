using System;
using UnityEngine;

[Serializable]
public abstract class CustomEffect
{
    [SerializeField] public string effectID;

    [SerializeField] protected float duration;
    [SerializeField] protected float currentDuration;
    [SerializeField] public bool uniqueEffect;

    public CustomEffect(string _effectID,float _duration, bool _uniqueEffect)
    {
        effectID = _effectID;
        duration = _duration;
        uniqueEffect = _uniqueEffect;
    }

    /// <summary>
    /// return true = need to be destroyed
    /// </summary>
    /// <returns></returns>
    public bool TimeEffectUpdate()
    {
        if (duration != -1)
        {
            currentDuration += Time.deltaTime;
            if (currentDuration >= duration)
            {
                return true;
            }
        }

        return false;
    }

    public abstract float ActivateEffect();
}

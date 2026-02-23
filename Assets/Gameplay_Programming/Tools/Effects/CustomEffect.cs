using System;
using UnityEngine;

[Serializable]
public abstract class CustomEffect
{
    [SerializeField] protected float duration;
    [SerializeField] protected float currentDuration;

    public CustomEffect(float _duration)
    {
        duration = _duration;
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

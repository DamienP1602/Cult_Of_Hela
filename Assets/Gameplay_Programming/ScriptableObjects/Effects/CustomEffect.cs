using System;
using UnityEngine;

[Serializable]
public abstract class CustomEffect : ScriptableObject
{
    public string effectID;

    public bool hasDuration;
    public float duration;
    public bool uniqueEffect;

    public CustomEffect(string _effectID,float _duration, bool _uniqueEffect)
    {
        effectID = _effectID;
        duration = _duration;
        uniqueEffect = _uniqueEffect;
    }

    public abstract float ActivateEffect();
}

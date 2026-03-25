using System;
using System.Collections.Generic;
using UnityEngine;

public struct SearchHitResult<T>
{
    public RaycastHit hit;
    public T component;

    public SearchHitResult(RaycastHit _hit, T _component)
    {
        hit = _hit;
        component = _component;
    }
}

public static class Macro
{
    public static SearchHitResult<T>? GetComponentFromHit<T>(RaycastHit[] _hits, List<T> _toIgnore = null)
    {
        foreach (RaycastHit _hit in _hits)
        {
            if (_hit.collider.GetComponent<T>() is T _result)
            {
                if (isInIgnoreList(_result, _toIgnore))
                    continue;

                return new SearchHitResult<T>(_hit,_result);
            }
        }
        return null;
    }

    static bool isInIgnoreList<T>(T _obj, List<T> _toIgnore)
    {
        if (_toIgnore == null) return false;

        foreach (T _ignoredObj in _toIgnore)
        {
            if (_obj.Equals(_ignoredObj))
                return true;
        }
        return false;
    }
}

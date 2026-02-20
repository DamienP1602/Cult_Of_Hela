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
    public static SearchHitResult<T>? GetComponentFromHit<T>(RaycastHit[] _hits)
    {
        foreach (RaycastHit _hit in _hits)
        {
            if (_hit.collider.GetComponent<T>() is T _result)
            {
                return new SearchHitResult<T>(_hit,_result);
            }
        }
        return null;
    }
}

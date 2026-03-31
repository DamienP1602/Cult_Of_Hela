using System.Collections.Generic;
using System.Linq;
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
    public struct Gizmo
    {
        public static void DrawLineSphere(Transform _origin, float _size, int _rayNumber, float _time)
        {
            float _angle = 360.0f / _rayNumber;

            for (int _i = 0; _i < _rayNumber; _i++)
            {
                Vector3 _end = Quaternion.AngleAxis(_angle * _i, _origin.up) * _origin.forward * _size;

                Vector3 _startRaycast = _origin.position + Vector3.up;
                Vector3 _endRaycast = _origin.position + _end + Vector3.up;

                Debug.DrawLine(_startRaycast, _endRaycast, Color.red, _time);
            }
        }
    }

    public struct CustomPhysics
    {
        public static bool LineSphereAll<T>(out List<T> _hits, Transform _origin, float _size, int _rayNumber, bool _showRays)
        {
            _hits = new List<T>();
            float _angle = 360.0f / _rayNumber;

            for (int _i = 0; _i < _rayNumber; _i++)
            {
                Vector3 _end = Quaternion.AngleAxis(_angle * _i, Vector3.up) * Vector3.forward * _size;

                RaycastHit[] _raycastResult = Physics.RaycastAll(_origin.position + Vector3.up, _end, _size);

                foreach (RaycastHit _result in _raycastResult)
                {
                    T _component = _result.collider.GetComponent<T>();
                    if (_component == null) continue;

                    if (!_hits.Contains(_component))
                        _hits.Add(_component);
                }
            }

            if (_showRays)
                Gizmo.DrawLineSphere(_origin, _size, _rayNumber, 5.0f);

            return _hits.Count > 0;
        }
    }

    public static SearchHitResult<T>? GetComponentFromHit<T>(RaycastHit[] _hits, List<T> _toIgnore = null)
    {
        foreach (RaycastHit _hit in _hits)
        {
            if (_hit.collider.GetComponent<T>() is T _result)
            {
                if (isInIgnoreList(_result, _toIgnore))
                    continue;

                return new SearchHitResult<T>(_hit, _result);
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

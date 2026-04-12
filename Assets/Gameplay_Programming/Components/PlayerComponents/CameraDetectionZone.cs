using System.Collections.Generic;
using UnityEngine;

public class CameraDetectionZone : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] List<GameObject> obstacles;

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            if (!obstacles.Contains(_other.gameObject))
            {
                MeshRenderer[] _childrens = _other.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer _renderer in _childrens)
                {
                    _renderer.enabled = false;
                }
                obstacles.Add(_other.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            if (obstacles.Contains(_other.gameObject))
            {
                MeshRenderer[] _childrens = _other.GetComponentsInChildren<MeshRenderer>();
                foreach (MeshRenderer _renderer in _childrens)
                {
                    _renderer.enabled = true;
                }
                obstacles.Remove(_other.gameObject);
            }
        }
    }
}

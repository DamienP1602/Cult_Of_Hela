using UnityEngine;

public class CameraZoneComponent : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool showDebug;

    [Header("Parameters")]
    [SerializeField] Vector3 newCameraPosition;
    [SerializeField] Vector3 oldCameraPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider _other)
    {
        if (_other.GetComponent<PlayerEntity>() is PlayerEntity _player)
        {
            oldCameraPosition = _player.CameraComponent.Offset;
            _player.CameraComponent.SetNewCameraOffset(newCameraPosition);
        }
    }

    private void OnTriggerExit(Collider _other)
    {
        if (_other.GetComponent<PlayerEntity>() is PlayerEntity _player)
        {
            _player.CameraComponent.SetNewCameraOffset(oldCameraPosition);
        }
    }

    private void OnDrawGizmos()
    {
        if (!showDebug) return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + newCameraPosition, 1.0f);
    }
}

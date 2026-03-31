using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraComponent : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] Camera currentCamera;
    [SerializeField] float cameraMoveSpeed = 5.0f;
    [SerializeField] Vector3 offset;
    [SerializeField] List<GameObject> obstacles;

    [Header("Debug")]
    [SerializeField] bool drawOffset;

    public Vector3 Offset => offset;

    private void Awake()
    {
        currentCamera = Camera.main;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
        LookAtTarget();
    }

    public void SetNewCameraOffset(Vector3 _offset) => offset= _offset;

    void FollowTarget()
    {
        currentCamera.transform.position = Vector3.MoveTowards(currentCamera.transform.position, transform.position + offset,Time.deltaTime * cameraMoveSpeed);
    }

    void LookAtTarget()
    {
        Vector3 _lookAt = transform.position - currentCamera.transform.position;
        if (_lookAt == Vector3.zero) return;

        Quaternion _rot = Quaternion.LookRotation(_lookAt);
        currentCamera.transform.rotation = Quaternion.RotateTowards(currentCamera.transform.rotation, _rot, Time.deltaTime * 100.0f);
    }

    private void OnTriggerEnter(Collider _other)
    {
        if (_other.gameObject.layer == LayerMask.NameToLayer("Buildings"))
        {
            if (!obstacles.Contains(_other.gameObject))
            {
                _other.gameObject.GetComponentInChildren<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
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
                _other.gameObject.GetComponentInChildren<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
                obstacles.Remove(_other.gameObject);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (!drawOffset) return;

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position + offset,0.5f);

        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + offset, transform.position);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 0.5f);
    }
}

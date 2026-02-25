using TMPro;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class DamageTextWorldWidget : MonoBehaviour
{
    TMP_Text damage;
    Camera mainCamera;
    CanvasGroup group;
    
    public CanvasGroup Group => group;

    private void Awake()
    {
        damage = GetComponentInChildren<TMP_Text>();
        mainCamera = Camera.main;
        group = GetComponent<CanvasGroup>();
    }

    public void InitText(int _damage)
    {
        damage.text = _damage.ToString();
    }

    private void Update()
    {
        MoveUpdate();
        RotationUpdate();
    }

    void MoveUpdate()
    {
        transform.position += Time.deltaTime * Vector3.up;
    }

    void RotationUpdate()
    {
        Vector3 _lookAt = transform.position - mainCamera.transform.position;
        if (_lookAt == Vector3.zero) return;

        transform.rotation = Quaternion.LookRotation(_lookAt);
    }
}

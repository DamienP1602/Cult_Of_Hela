using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.VFX;

public class PlayerClickComponent : MonoBehaviour
{
    PlayerEntity owner;

    [Header("Parameters")]
    [SerializeField] bool isClick;
    [SerializeField] VisualEffectAsset onClickEffect;
    [SerializeField] bool canClick = true;
    [SerializeField] bool isInUI = false;

    Ray PointOnScreen => Camera.main.ScreenPointToRay(Input.mousePosition);

    public void SetIsClick(bool _value) => isClick = _value;
    public void SetCanClick(bool _value) => canClick = _value;
    public void SetIsInUI(bool _value) => isInUI = _value;

    private void Awake()
    {
        owner = GetComponent<PlayerEntity>();
    }

    private void Update()
    {
        ClickUpdate();
    }

    void ClickUpdate()
    {
        if (isClick && canClick && !isInUI)
        {
            RaycastHit[] _hits = Physics.RaycastAll(PointOnScreen, 100.0f);
            if (_hits.Length == 0) return;

            if (Macro.GetComponentFromHit<GameEntity>(_hits) is SearchHitResult<GameEntity> _enemyHit)
            {
                if (_enemyHit.component != owner)
                    owner.InteractionComponent.SetTarget(_enemyHit.component);
            }
            else if (Macro.GetComponentFromHit<Terrain>(_hits) is SearchHitResult<Terrain> _groundHit)
            {
                owner.MovementComponent.SetDestination(_groundHit.hit.point);
            }
        }
    }

    public Vector3 GetMousePositionOnWorld()
    {
        if (Physics.Raycast(PointOnScreen, out RaycastHit _hit, 100.0f))
        {
            return _hit.point;
        }

        return Vector3.zero;
    }

    public void SpawnClickVFX()
    {
        if (!canClick || isInUI) return;

        bool _hasHit = Physics.Raycast(PointOnScreen, out RaycastHit _hit, 100.0f);
        if (_hasHit && _hit.collider.GetComponent<Terrain>())
        {
            VisualEffect _effect = Instantiate(GameManager.Instance.EmptyVisualEffect, _hit.point + Vector3.up * 0.05f, Quaternion.identity);

            _effect.visualEffectAsset = onClickEffect;
            Destroy(_effect.gameObject, 1.0f);
        }
    }
}

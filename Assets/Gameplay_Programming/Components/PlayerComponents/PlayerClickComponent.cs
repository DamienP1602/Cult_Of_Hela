using UnityEngine;

public class PlayerClickComponent : MonoBehaviour
{
    PlayerEntity owner;

    [Header("Parameters")]
    [SerializeField] bool isClick;
    
    Ray PointOnScreen => Camera.main.ScreenPointToRay(Input.mousePosition);

    public void SetIsClick(bool _value) => isClick = _value;

    private void Awake()
    {
        owner = GetComponent<PlayerEntity>();
        InvokeRepeating(nameof(ClickUpdate), 0.2f,0.2f);
    }

    private void Update()
    {

    }

    void ClickUpdate()
    {
        if (isClick)
        {
            RaycastHit[] _hits = Physics.RaycastAll(PointOnScreen, 100.0f);
            if (_hits.Length == 0) return;

            if (Macro.GetComponentFromHit<GameEntity>(_hits) is SearchHitResult<GameEntity> _enemyHit)
            {
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
        if (Physics.Raycast(PointOnScreen,out RaycastHit _hit, 100.0f))
        {
            return _hit.point;
        }

        return Vector3.zero;
    }
}

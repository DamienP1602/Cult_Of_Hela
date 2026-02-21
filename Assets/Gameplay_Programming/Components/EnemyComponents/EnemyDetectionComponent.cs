using UnityEngine;

public class EnemyDetectionComponent : MonoBehaviour
{
    [Header("Debug")]
    [SerializeField] bool drawRange = false;

    [Header("Parameters")]
    [SerializeField] float detectionRange = 1.0f;
    [SerializeField] GameEntity target;

    public GameEntity Target => target;

    void Start()
    {

    }

    public bool IsPlayerInDetectionRange()
    {
        PlayerEntity _player = GameManager.Instance.Player;
        if (!_player) return false;

        bool _isInRange = Vector3.Distance(transform.position, _player.transform.position) <= detectionRange;

        if (_isInRange)
            target = _player;

        return _isInRange;
    }

    public void ResetTarget() => target = null;

    private void OnDrawGizmos()
    {
        if (!drawRange) return;

        Gizmos.color = Color.yellow;

        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
}

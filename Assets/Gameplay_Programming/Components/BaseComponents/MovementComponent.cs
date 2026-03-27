using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.GridLayoutGroup;

[RequireComponent(typeof(NavMeshAgent))]
public class MovementComponent : MonoBehaviour
{
    NavMeshAgent agent;
    AnimationComponent animRef;

    [Header("Debug")]
    [SerializeField] bool drawDebug;

    [Header("Components")]
    [SerializeField] Vector3 destination;
    [SerializeField] bool isAtDestination;
    [SerializeField] float forceRotationSpeed = 1.0f;
    [SerializeField] GameEntity target;
    [SerializeField] Vector3? rotateTo;
    [SerializeField] bool canMove = true;

    [Header("Dash Parameters")]
    [SerializeField] bool isDashing = false;
    [SerializeField] float dashPower = 3.0f;
    [SerializeField] AnimationCurve curve;
    [SerializeField] float dashTime = 0.5f;
    [field:SerializeField] public bool CanDash = true;
    float currentDashTime;

    public bool AtDestination => isAtDestination;

    public bool IsNearDestination() => Vector3.Distance(new Vector3(agent.destination.x, 0.0f, agent.destination.z), new Vector3(transform.position.x, 0.0f, transform.position.z)) < 0.1f;


    public void SetCanMove(bool _value) => canMove = _value;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animRef = GetComponent<AnimationComponent>();
    }

    private void Update()
    {
        if (!agent.enabled) return;

        ReachDestinationUpdate();
        RotationUpdate();

        if (isDashing)
        {
            DashUpdate();
        }
    }

    void DashUpdate()
    {
        currentDashTime += Time.deltaTime;
        if (currentDashTime >= dashTime)
        {
            animRef.SetBool("dash", false);
            isDashing = false;
        }

        if (!FoundObstacle())
        {
            float _value = curve.Evaluate(currentDashTime) * dashPower * Time.deltaTime;
            transform.position += transform.forward * _value;
        }
    }

    bool FoundObstacle()
    {
        RaycastHit[] _hits = Physics.RaycastAll(new Ray(transform.position + Vector3.up * 0.5f,transform.forward),1.0f);

        if (Macro.GetComponentFromHit<GameEntity>(_hits, new List<GameEntity>() { GetComponent<GameEntity>() }) is SearchHitResult<GameEntity> _obstacle)
                return true;

        return false;
    }

    public void SetRotationTarget(Vector3 _target)
    {
        rotateTo = _target;
    }

    void ReachDestinationUpdate()
    {
        if (!isAtDestination)
        {
            if (IsNearDestination())
            {
                isAtDestination = true;
                animRef.SetBool("movement", false);

                if (target)
                    target = null;
            }
        }
    }

    void RotationUpdate()
    {
        if (rotateTo == null) return;

        Vector3 _lookAt = rotateTo.Value - transform.position;
        Vector3 _newLookAt = new Vector3(_lookAt.x, 0.0f, _lookAt.z);

        Quaternion _rot = Quaternion.LookRotation(_newLookAt);

        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, forceRotationSpeed);
    }

    void FollowTargetUpdate()
    {
        if (!target || !agent.enabled) return;

        if (!canMove) return;

        Vector3 _destination = target.transform.position;
        bool _succeed = NavMesh.SamplePosition(_destination, out NavMeshHit _hit, 100.0f, -1);
        if (_succeed)
            agent.SetDestination(_hit.position);

        isAtDestination = false;

        destination = agent.destination;
    }

    public void SetDestination(Vector3 _destination)
    {
        if (!agent.enabled || isDashing) return;

        if (!canMove) return;

        bool _succeed = NavMesh.SamplePosition(_destination, out NavMeshHit _hit, 100.0f, -1);
        if (_succeed)
            agent.SetDestination(_hit.position);

        destination = agent.destination;
        agent.isStopped = false;
        isAtDestination = false;

        if (_succeed)
        {
            animRef.SetBool("movement", true);
            animRef.SetBool("attack", false);
            animRef.SetBool(animRef.CurrentSpellAnimName, false);
        }

        rotateTo = null;
        target = null;
    }

    public void SetTarget(GameEntity _entity)
    {
        if (!agent.enabled || isDashing) return;

        if (!canMove) return;

        Vector3 _destination = _entity.transform.position;
        bool _succeed = NavMesh.SamplePosition(_destination, out NavMeshHit _hit, 100.0f, -1);
        if (_succeed)
            agent.SetDestination(_hit.position);

        destination = agent.destination;
        agent.isStopped = false;

        if (_succeed)
        {
            animRef.SetBool("movement", true);
            animRef.SetBool("attack", false);
            animRef.SetBool(animRef.CurrentSpellAnimName, false);
        }

        if (!target)
            InvokeRepeating(nameof(FollowTargetUpdate), 0.2f, 0.2f);

        rotateTo = null;
        target = _entity;
    }

    public void ResetTarget()
    {
        target = null;
        rotateTo = null;
    }

    public void StopMovement()
    {
        if (!agent.enabled) return;

        agent.isStopped = true;
        animRef.SetBool("movement", false);
    }

    public void SetDash()
    {
        if (isDashing || !CanDash) return;

        ResetTarget();
        agent.isStopped = true;
        isAtDestination = true;
        isDashing = true;
        currentDashTime = 0.0f;

        animRef.SetBool("dash", true);
        animRef.SetBool("movement", false);
        animRef.SetBool("attack", false);
        animRef.SetBool(animRef.CurrentSpellAnimName, false);
    }

    private void OnDrawGizmos()
    {
        if (!drawDebug) return;

        NavMeshPath _path = agent.path;

        List<Vector3> _points = _path.corners.ToList();
        if (_points.Count > 0)
        {
            for (int _i = 0; _i < _points.Count; _i++)
            {
                if (_i + 1 < _points.Count)
                {
                    Gizmos.color = Color.green;
                    Gizmos.DrawLine(_points[_i], _points[_i + 1]);
                }
                Gizmos.DrawWireSphere(_points[_i], 0.5f);
            }
        }
    }
}

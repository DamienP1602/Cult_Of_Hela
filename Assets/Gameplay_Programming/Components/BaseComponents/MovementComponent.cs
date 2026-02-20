using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

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
    [SerializeField] GameEntity target;
    [SerializeField] Vector3? rotateTo;

    public bool IsNearDestination() => Vector3.Distance(new Vector3(destination.x, 0.0f, destination.z), new Vector3(transform.position.x, 0.0f, transform.position.z)) < 0.1f;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animRef = GetComponent<AnimationComponent>();
    }

    private void Update()
    {
        ReachDestinationUpdate();
        RotationUpdate();
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

        transform.rotation = Quaternion.RotateTowards(transform.rotation, _rot, 2.0f);
    }

    void FollowTargetUpdate()
    {
        if (!target) return;

        Vector3 _destination = target.transform.position;
        bool _succeed = agent.SetDestination(_destination);
        destination = _destination;
    }

    public void SetDestination(Vector3 _destination)
    {
        bool _succeed = agent.SetDestination(_destination);
        destination = _destination;
        agent.isStopped = false;
        isAtDestination = false;

        if (_succeed)
        {
            animRef.SetBool("movement", true);
            animRef.SetBool("attack", false);
        }

        rotateTo = null;
        target = null;
    }

    public void SetTarget(GameEntity _entity)
    {
        Vector3 _destination = _entity.transform.position;
        bool _succeed = agent.SetDestination(_destination);
        destination = _destination;
        agent.isStopped = false;
        isAtDestination = false;

        if (_succeed)
            animRef.SetBool("movement", true);

        if (!target)
            InvokeRepeating(nameof(FollowTargetUpdate), 0.2f, 0.2f);

        target = _entity;
    }

    public void StopMovement()
    {
        agent.isStopped = true;
        animRef.SetBool("movement", false);
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

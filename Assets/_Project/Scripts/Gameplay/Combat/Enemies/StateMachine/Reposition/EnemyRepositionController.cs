using System;
using _Project.Scripts.Utilities;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace _Project.Scripts.Combat.BaseEnemy.Reposition {
    public class EnemyRepositionController : MonoBehaviour {
    public bool IsRunning { get; private set; }
    public bool IsFinished => !IsRunning;

    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private float defaultFlexibility = 0.5f;
    [SerializeField] private Vector2 repositionRange = new Vector2(5f, 8f);
    [SerializeField] private float chaseTime = 5f;
    [SerializeField] private PlayerDetector playerDetector;

    private float _currentFlexibility;
    private Transform _target;
    private bool _hasDestination;
    
    private void Update()
    {
        if (!CanUpdate())
            return;

        if (_hasDestination)
        {
            TryFinishReposition();
            return;
        }

        TryPickDestination();
    }

    private bool CanUpdate()
    {
        return IsRunning && _target;
    }

    private void TryFinishReposition()
    {
        if (!HasReachedDestination())
            return;

        _hasDestination = false;
        StopReposition();
    }

    private void TryPickDestination()
    {
        if (!HasReachedDestination())
            return;

        Vector3 candidatePosition = GetCandidatePosition();

        if (!NavMesh.SamplePosition(candidatePosition, out NavMeshHit hit, repositionRange.y, NavMesh.AllAreas))
            return;

        agent.SetDestination(hit.position);
        _hasDestination = true;
    }

    private Vector3 GetCandidatePosition()
    {
        Vector3 direction = GetRandomDirection();
        float distance = Random.Range(repositionRange.x, repositionRange.y);
        return transform.position + direction * distance;
    }

    private bool HasReachedDestination()
    {
        return !agent.pathPending &&
               agent.remainingDistance <= agent.stoppingDistance &&
               (!agent.hasPath || agent.velocity.sqrMagnitude == 0f);
    }

    private Vector3 GetRandomDirection()
    {
        Vector3 directionToTarget = _target.position - transform.position;
        directionToTarget.y = 0f;

        if (directionToTarget.sqrMagnitude < 0.001f)
            directionToTarget = transform.forward;

        float maxAngle = Mathf.Lerp(0f, 180f, _currentFlexibility);
        float sampledAngle = Random.Range(-maxAngle, maxAngle);

        Vector3 sampledDirection = Quaternion.AngleAxis(sampledAngle, Vector3.up) * directionToTarget.normalized;

        return sampledDirection;
    }

    public void BeginReposition(Transform target)
    {
        _target = target;
        _currentFlexibility = defaultFlexibility;
        _hasDestination = false;
        IsRunning = true;
    }

    public void StopReposition()
    {
        IsRunning = false;
        _target = null;
        _hasDestination = false;
    }
    }   
}
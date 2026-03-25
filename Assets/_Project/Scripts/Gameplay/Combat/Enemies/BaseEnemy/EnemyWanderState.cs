using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.BaseEnemy {
    public class EnemyWanderState : EnemyBaseState {
    private readonly float _wanderRadius;
    private readonly NavMeshAgent _agent;
    private readonly Vector3 _startPosition;
    private readonly Collider _wanderZone;

    private bool _walkingBackToZone;

    public EnemyWanderState(
        Enemy enemy,
        Animator animator,
        float wanderRadius,
        NavMeshAgent agent,
        Collider wanderZone,
        Transform startPosition) : base(enemy, animator)
    {
        _wanderRadius = wanderRadius;
        _agent = agent;
        _wanderZone = wanderZone;
        _startPosition = startPosition.position;
    }

    public override void Update()
    {
        base.Update();

        if (IsOutsideWanderZone()) {
            HandleReturnToZone();
            return;
        }
        _walkingBackToZone = false;
        if (HasReachedDestination()) {
            TrySetRandomDestination();
        }
    }

    private bool IsOutsideWanderZone() {
        return !_wanderZone.bounds.Contains(_agent.transform.position);
    }

    private void HandleReturnToZone()
    {
        if (_walkingBackToZone)
            return;

        if (NavMesh.SamplePosition(_startPosition, out NavMeshHit hit, _wanderRadius, NavMesh.AllAreas))
        {
            _agent.SetDestination(hit.position);
            _walkingBackToZone = true;
        }
    }

    private void TrySetRandomDestination()
    {
        Vector3 randomOffset = Random.insideUnitSphere * _wanderRadius;
        randomOffset.y = 0f;

        Vector3 candidatePosition = _startPosition + randomOffset;

        if (!NavMesh.SamplePosition(candidatePosition, out NavMeshHit hit, _wanderRadius, NavMesh.AllAreas))
            return;

        if (!_wanderZone.bounds.Contains(hit.position))
            return;

        _agent.SetDestination(hit.position);
    }

    private bool HasReachedDestination()
    {
        return !_agent.pathPending &&
               _agent.remainingDistance <= _agent.stoppingDistance &&
               (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f);
    }

    public override void OnExit() {
    }
    }
}
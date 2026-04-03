using _Project.Scripts.Combat.HSM.Structs;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.HSM.Motors {
    public class WanderMotor {
        private readonly float _wanderRadius;
        private readonly NavMeshAgent _agent;
        private readonly Vector3 _startPosition;
        private readonly Collider _wanderZone;
        private Animator _animator;
        private bool _walkingBackToZone;

        public WanderMotor(WanderDeps wanderDeps, NavMeshAgent agent, Animator animator) {
            _wanderRadius = wanderDeps.wanderRadius;
            _agent = agent;
            _startPosition = wanderDeps.startPosition;
            _wanderZone = wanderDeps.wanderZone;
            _animator = animator;
        }

        public void OnEnter() {
            _animator.SetTrigger(EnemyRoot.WalkHash);
        }

        public void Update(float deltaTime) {
            UpdateAnimation();
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

            if (NavMesh.SamplePosition(_startPosition, out NavMeshHit hit, _wanderRadius, NavMesh.AllAreas)) {
                _agent.SetDestination(hit.position);
                _walkingBackToZone = true;
            }
        }

        private void UpdateAnimation() {
            _animator.SetFloat(EnemyRoot.VelocityHash, _agent.velocity.normalized.magnitude);
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
        public void Exit() => _walkingBackToZone = false;
        private bool HasReachedDestination()
        {
            return !_agent.pathPending &&
                   _agent.remainingDistance <= _agent.stoppingDistance &&
                   (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f);
        }
    }
}
using UnityEngine;
using UnityEngine.AI;
using _Project.Scripts.Combat.BaseEnemy;

namespace _Project.Scripts.Combat.HSM {
    public class RepositionMotor {
        public RepositionMotor(RepositionDeps repositionDeps, Transform source, NavMeshAgent agent, Animator animator) {
            _source = source;
            _agent = agent;
            _flexibility = repositionDeps.flexibility;
            _repositionRange =  repositionDeps.repositionRange;
            _animator = animator;
        }

        public bool IsRunning { get; private set; }
        public bool IsFinished => !IsRunning;
        private readonly NavMeshAgent _agent;
        private readonly float _flexibility;
        private readonly Vector2 _repositionRange;
        
        private Transform _target;
        private readonly Transform _source;
        private bool _hasDestination;
        private Animator _animator;

        public void OnEnter() {
            _animator.SetTrigger(EnemyRoot.WalkHash);
        }

        public void Update(float deltaTime) {
            if (!CanUpdate())
                return;

            if (_hasDestination) {
                TryFinishReposition();
                return;
            }

            TryPickDestination();
        }

        private bool CanUpdate() {
            return IsRunning && _target;
        }

        private void TryFinishReposition() {
            if (!HasReachedDestination())
                return;

            _hasDestination = false;
            StopReposition();
        }

        private void TryPickDestination() {
            if (!HasReachedDestination())
                return;

            Vector3 candidatePosition = GetCandidatePosition();

            if (!NavMesh.SamplePosition(candidatePosition, out NavMeshHit hit, _repositionRange.y, NavMesh.AllAreas))
                return;

            _agent.SetDestination(hit.position);
            _animator.SetFloat(EnemyRoot.WalkHash, _agent.velocity.normalized.magnitude);
            _hasDestination = true;
        }

        private Vector3 GetCandidatePosition() {
            Vector3 direction = GetRandomDirection();
            float distance = Random.Range(_repositionRange.x, _repositionRange.y);
            return _source.position + direction * distance;
        }

        private bool HasReachedDestination() {
            return !_agent.pathPending &&
                   _agent.remainingDistance <= _agent.stoppingDistance &&
                   (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f);
        }

        private Vector3 GetRandomDirection() {
            Vector3 directionToTarget = _target.position - _source.position;
            directionToTarget.y = 0f;

            if (directionToTarget.sqrMagnitude < 0.001f)
                directionToTarget = _source.forward;
            
            float sampledAngle = Random.Range(-_flexibility, _flexibility);

            Vector3 sampledDirection = Quaternion.AngleAxis(sampledAngle, Vector3.up) * directionToTarget.normalized;

            return sampledDirection;
        }

        public void BeginReposition(Transform target) {
            _target = target;
            _hasDestination = false;
            IsRunning = true;
        }

        public void StopReposition() {
            IsRunning = false;
            _target = null;
            _hasDestination = false;
        }
    }
}
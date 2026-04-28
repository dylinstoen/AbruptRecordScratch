using UnityEngine;
using UnityEngine.AI;
using _Project.Scripts.Combat.BaseEnemy;

namespace _Project.Scripts.Combat.HSM {
    public class RangedRepositionMotor : RepositionMotor {
        private readonly float _flexibility;
        private readonly Vector2 _repositionRange;        
        private readonly Transform _source;

        public RangedRepositionMotor(RepositionDeps repositionDeps, Transform source, NavMeshAgent agent, Animator _animator) : base(_animator, agent) {
            _source = source;
            _flexibility = repositionDeps.flexibility;
            _repositionRange =  repositionDeps.repositionRange;
        }

        protected override void TryFinishReposition() {
            if (!HasReachedDestination())
                return;

            _hasDestination = false;
            StopReposition();
        }

        protected override void TryPickDestination() {
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


        private Vector3 GetRandomDirection() {
            Vector3 directionToTarget = _target.position - _source.position;
            directionToTarget.y = 0f;

            if (directionToTarget.sqrMagnitude < 0.001f)
                directionToTarget = _source.forward;
            
            float sampledAngle = Random.Range(-_flexibility, _flexibility);

            Vector3 sampledDirection = Quaternion.AngleAxis(sampledAngle, Vector3.up) * directionToTarget.normalized;

            return sampledDirection;
        }
    }
}
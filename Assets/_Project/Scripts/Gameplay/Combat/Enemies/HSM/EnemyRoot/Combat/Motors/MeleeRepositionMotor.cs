using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

namespace _Project.Scripts.Combat.HSM {
    public class MeleeRepositionMotor : RepositionMotor {
        // Melee will reposition around the target
        // Lets say there is a radius of 1 around the source and i want to travel 2 units around the source, convert units to travel to radiants?
        private readonly float _repositionRadiusAroundTarget;
        private readonly Transform _source;
        private readonly RepositionDeps.RepositionDirection _repositionDirection;
        private readonly Vector2 _repositionRange;


        private int _steps = 10;
        private int _currentStep = 0;
        public MeleeRepositionMotor(RepositionDeps repositionDeps, Transform source, NavMeshAgent agent, Animator animator) : base(animator, agent) {
            _source = source;
            _repositionRadiusAroundTarget =  repositionDeps.repositionRadiusAroundTarget;
            _repositionDirection = repositionDeps.repositionDirection;
            _repositionRange = repositionDeps.repositionRange;
        }

        Vector3 candidatePosition;
        protected override void TryPickDestination() {
            if (!HasReachedDestination())
                return;

            if (_currentStep >= _steps) {
                StopReposition();
                return;
            }

            candidatePosition = GetCandidatePosition();

            if (!NavMesh.SamplePosition(candidatePosition, out NavMeshHit hit, _repositionRadiusAroundTarget, NavMesh.AllAreas))
                return;
            
            _agent.SetDestination(hit.position);
            _animator.SetFloat(EnemyRoot.WalkHash, _agent.velocity.normalized.magnitude);
            _hasDestination = true;
            _currentStep++;
        }

        private Vector3 GetCandidatePosition() {
            Vector3 fromTargetToEnemy = _source.position - _target.position;
            fromTargetToEnemy.y = 0f;

            if (fromTargetToEnemy.sqrMagnitude < 0.001f)
                return _source.position;

            float totalAngleRad = Random.Range(_repositionRange.x, _repositionRange.y) / _repositionRadiusAroundTarget;
            float stepAngleDeg = (totalAngleRad / _steps) * Mathf.Rad2Deg;

            if (_repositionDirection == RepositionDeps.RepositionDirection.Right)
                stepAngleDeg = -stepAngleDeg;

            Quaternion rotation = Quaternion.AngleAxis(stepAngleDeg, Vector3.up);

            Vector3 currentDir = fromTargetToEnemy.normalized;
            Vector3 newDir = rotation * currentDir;

            return _target.position + newDir * (_repositionRadiusAroundTarget + 0.2f);
        }
        void OnDrawGizmos() {
            Gizmos.DrawLine(_source.transform.position, candidatePosition);
        }
        protected override void TryFinishReposition() {
            if (!HasReachedDestination())
                return;

            _hasDestination = false;
            if(_currentStep < _steps) {
                return;
            }
            StopReposition();
        }
        public override void BeginReposition(Transform target) {
            base.BeginReposition(target);
            _agent.autoBraking = false;
            _currentStep = 0;
        }
    }
}
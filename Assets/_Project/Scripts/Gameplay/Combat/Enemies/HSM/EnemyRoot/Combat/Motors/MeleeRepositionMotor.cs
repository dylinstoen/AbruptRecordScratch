using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.HSM {
    public class MeleeRepositionMotor : RepositionMotor {
        // Melee will reposition around the target
        // Lets say there is a radius of 1 around the source and i want to travel 2 units around the source, convert units to travel to radiants?
        private readonly float _repositionRadiusAroundTarget;
        private readonly Transform _source;

        public MeleeRepositionMotor(RepositionDeps repositionDeps, Transform source, NavMeshAgent agent, Animator animator) : base(animator, agent) {
            _source = source;
            _repositionRadiusAroundTarget =  repositionDeps.repositionRadiusAroundTarget;
        }

        protected override void TryPickDestination() {
            if (!HasReachedDestination())
                return;
            
            Vector3 candidatePosition = GetCandidatePosition();
            
            if (!NavMesh.SamplePosition(candidatePosition, out NavMeshHit hit, _repositionRadiusAroundTarget, NavMesh.AllAreas))
                return;
            
            _agent.SetDestination(hit.position);
            _animator.SetFloat(EnemyRoot.WalkHash, _agent.velocity.normalized.magnitude);
            _hasDestination = true;
        }
        
        private Vector3 GetCandidatePosition() {
            Vector3 direction = GetDirection();
            float distance = _repositionRadiusAroundTarget;
            return _source.position + direction * distance;
        }

        private Vector3 GetDirection() {
            return Vector3.zero;
        }
    }
}
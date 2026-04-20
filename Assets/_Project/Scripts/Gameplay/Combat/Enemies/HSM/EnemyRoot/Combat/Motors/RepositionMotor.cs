using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.HSM {
    public abstract class RepositionMotor {
        protected Animator _animator;
        protected Transform _target;
        protected bool _hasDestination;
        protected NavMeshAgent _agent;
        public bool IsFinished() => !IsRunning;
        public bool IsRunning { get; protected set; }

        protected RepositionMotor(Animator animator, NavMeshAgent agent) {
            _animator = animator;
            _agent = agent;
        }
        public void OnEnter() {
            _animator.SetTrigger(EnemyRoot.WalkHash);
        }
        public virtual void Update(float deltaTime) {
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

        protected abstract void TryPickDestination();

        protected bool HasReachedDestination() {
            return !_agent.pathPending &&
                   _agent.remainingDistance <= _agent.stoppingDistance &&
                   (!_agent.hasPath || _agent.velocity.sqrMagnitude == 0f);
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
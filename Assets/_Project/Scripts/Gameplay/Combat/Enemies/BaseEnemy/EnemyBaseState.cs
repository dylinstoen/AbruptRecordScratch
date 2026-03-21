

using _Project.Scripts.Utilities.StateMachine.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Combat.BaseEnemy {
    public abstract class EnemyBaseState : IState {
        protected readonly Enemy _enemy;
        protected readonly Animator _animator;
        
        private static readonly int idleHash =  Animator.StringToHash("idle");
        private static readonly int walkHash =  Animator.StringToHash("walk");
        private static readonly int dieHash =  Animator.StringToHash("die");

        protected float _crossFadeDuration = 0.1f;
        
        protected EnemyBaseState(Enemy enemy, Animator animator) {
            _enemy = enemy;
            _animator = animator;
        }
        public virtual void OnEnter() {
            
        }

        public virtual void OnExit() {
            
        }

        public virtual void Update() {
            
        }

        public virtual void FixedUpdate() {
            
        }
    }
}
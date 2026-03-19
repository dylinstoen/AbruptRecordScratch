

using _Project.Scripts.Utilities.StateMachine.Interfaces;
using UnityEngine;

namespace _Project.Scripts.Combat.BaseEnemy {
    public class EnemyBaseState : IState {
        protected readonly Enemy _enemy;
        protected readonly Animator _animator;

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
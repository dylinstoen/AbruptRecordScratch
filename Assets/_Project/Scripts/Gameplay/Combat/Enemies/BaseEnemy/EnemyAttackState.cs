using _Project.Scripts.Actors;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.BaseEnemy {
    public class EnemyAttackState : EnemyBaseState {
        private NavMeshAgent _agent;
        private Transform _player;
        public EnemyAttackState(Enemy enemy, Animator animator, NavMeshAgent agent, Transform player) : base(enemy, animator) {
            _agent = agent;
            _player = player;
        }

        public override void Update() {
            base.Update();
            _enemy.Attack();
        }

        public bool FinishedAttacking() {
            return false;
        }
    }
}
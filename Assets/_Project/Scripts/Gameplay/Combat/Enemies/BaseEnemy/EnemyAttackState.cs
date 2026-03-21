using _Project.Scripts.Actors;
using _Project.Scripts.Core;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.BaseEnemy {
    public class EnemyAttackState : EnemyBaseState {
        private NavMeshAgent _agent;
        private Transform _player;
        public EnemyAttackState(Enemy enemy, Animator animator, NavMeshAgent agent) : base(enemy, animator) {
            _agent = agent;
        }

        public override void OnEnter() {
            base.OnEnter();
            _player = SceneServiceLocator.Current.Player.PlayerFacade.Root;
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
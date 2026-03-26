using _Project.Scripts.Actors;
using _Project.Scripts.Combat.BaseEnemy.Attack;
using _Project.Scripts.Core;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.BaseEnemy {
    public class EnemyAttackState : EnemyBaseState {
        private NavMeshAgent _agent;
        private IPlayerFacade _playerFacade;
        private EnemyAttackController _attackController;
        private bool _startedAttack = false;
        public EnemyAttackState(Enemy enemy, Animator animator, NavMeshAgent agent, EnemyAttackController attackController) : base(enemy, animator) {
            _agent = agent;
            _attackController = attackController;
        }

        public override void OnEnter() {
            base.OnEnter();
            
            _playerFacade = SceneServiceLocator.Current.Player.PlayerFacade;
            _startedAttack = false;
            
            _agent.isStopped = true;
            _agent.ResetPath();
        }

        public override void Update() {
            base.Update();
            if (_playerFacade == null) {
                _playerFacade = SceneServiceLocator.Current.Player.PlayerFacade;
                return;
            }
                
            if (!_startedAttack) {
                _attackController.BeginAttck(_playerFacade.Root);
                _startedAttack = true;
            }
        }

        public override void OnExit() {
            base.OnExit();
            _agent.isStopped = false;
            _attackController.StopAttack();
        }

        public bool IsFinished() {
            return _startedAttack && _attackController.IsFinished;
        }
    }
}
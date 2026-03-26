using _Project.Scripts.Actors;
using _Project.Scripts.Combat.BaseEnemy;
using _Project.Scripts.Core;
using _Project.Scripts.Utilities.HSM;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.HSM {
    public class Attack : State {
        private PlayerDetector _playerDetector;
        private AttackMotor _attackMotor;
        private IPlayerFacade _playerFacade;
        private bool _startedAttack;
        private NavMeshAgent _agent;
        public Attack(StateMachine stateMachine, State parent, Transform source, NavMeshAgent agent, AttackDeps attackDeps, PlayerDetector playerDetector) : base(stateMachine, parent) {
            _playerDetector = playerDetector;
            _attackMotor = new AttackMotor(attackDeps, source);
            _agent = agent;
        }

        protected override void OnEnter() {
            _playerFacade = SceneServiceLocator.Current.Player.PlayerFacade;
            _startedAttack = false;
            
            _agent.isStopped = true;
            _agent.ResetPath();
        }

        protected override void OnUpdate(float deltaTime) {
            if (_playerFacade == null) {
                _playerFacade = SceneServiceLocator.Current.Player.PlayerFacade;
                return;
            }
                
            if (!_startedAttack) {
                _attackMotor.BeginAttck(_playerFacade.Root);
                _startedAttack = true;
            }
            _attackMotor.Update(deltaTime);
        }

        protected override void OnExit() {
            
            _agent.isStopped = false;
            _attackMotor.StopAttack();
        }

        protected override State GetTransition() {
            var parent = (Combat)Parent;
            if (_startedAttack && _attackMotor.IsFinished) {
                return parent.Reposition;
            }
            return null;
        }
    }
}
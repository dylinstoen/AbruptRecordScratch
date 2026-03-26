using _Project.Scripts.Actors;
using _Project.Scripts.Combat.BaseEnemy.Reposition;
using _Project.Scripts.Core;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.BaseEnemy {
    public class EnemyRepositionState : EnemyBaseState {
        private EnemyRepositionController _repositionController;
        private readonly NavMeshAgent _agent;
        private IPlayerFacade _playerFacade;
        private bool _startedReposition = false;
        public EnemyRepositionState(Enemy enemy, Animator animator, NavMeshAgent agent, EnemyRepositionController repositionController) :
            base(enemy, animator) {
            _repositionController = repositionController;
            _agent = agent;
            _startedReposition = false;
        }

        public override void OnEnter() {
            base.OnEnter();
            
            _playerFacade = SceneServiceLocator.Current.Player.PlayerFacade;
            _startedReposition = false;
            
            _agent.ResetPath(); 
            _agent.isStopped = false;
        }

        public override void Update() {
            base.Update();
            if (_playerFacade == null) {
                _playerFacade = SceneServiceLocator.Current.Player.PlayerFacade;
                return;
            }
            if (!_startedReposition) {
                _repositionController.BeginReposition(_playerFacade.Root);
                _startedReposition = true;
            }
        }

        public override void OnExit() {
            base.OnExit();
            
            _startedReposition = false;
            _repositionController.StopReposition();
            
            _agent.ResetPath();
            _agent.isStopped = true;
        }

        public bool IsFinished() {
            return _startedReposition && _repositionController.IsFinished;
        }
    }
}
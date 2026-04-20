using _Project.Scripts.Actors;
using _Project.Scripts.Combat.BaseEnemy;
using _Project.Scripts.Core;
using _Project.Scripts.Utilities;
using _Project.Scripts.Utilities.HSM;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.HSM {
    public class Reposition : State {
        private PlayerDetector _playerDetector;
        private IPlayerFacade _playerFacade;
        private readonly RepositionMotor _repositionMotor;
        private readonly NavMeshAgent _agent;
        private bool _startedReposition;
        private CountdownTimer _timeToLiveTimer;
        public Reposition(StateMachine stateMachine, State parent, Transform source, RepositionDeps repositionDeps, NavMeshAgent agent, PlayerDetector playerDetector, Animator animator) : base(stateMachine, parent) {
            _playerDetector = playerDetector;
            var type = repositionDeps.repositionType;
            _repositionMotor = type == RepositionDeps.RepositionType.Melee
                ? new MeleeRepositionMotor(repositionDeps, source, agent, animator)
                : new RangedRepositionMotor(repositionDeps, source, agent, animator);
            _agent = agent;
            _timeToLiveTimer = new CountdownTimer(repositionDeps.timeToLive);
        }

        protected override void OnEnter() {
            _playerFacade = SceneServiceLocator.Current.Player.PlayerFacade;
            _startedReposition = false;
            _repositionMotor.OnEnter();
            _agent.isStopped = false;
            _agent.ResetPath(); 
            _timeToLiveTimer.Start();
        }

        protected override void OnUpdate(float deltaTime) {
            if (_playerFacade == null) {
                _playerFacade = SceneServiceLocator.Current.Player.PlayerFacade;
                return;
            }
            if (!_startedReposition) {
                _repositionMotor.BeginReposition(_playerFacade.Root);
                _startedReposition = true;
            }
            _timeToLiveTimer.Tick(deltaTime);
            _repositionMotor.Update(deltaTime);
        }

        protected override void OnExit() {
            _startedReposition = false;
            _repositionMotor.StopReposition();
        }

        protected override State GetTransition() {
            var parent = (Combat)Parent;
            if (_startedReposition && (_repositionMotor.IsFinished() || _timeToLiveTimer.IsFinished)) {
                return parent.Attack;
            }
            return null;
        }
    }
}
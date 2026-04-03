using _Project.Scripts.Actors;
using _Project.Scripts.Combat.BaseEnemy;
using _Project.Scripts.Combat.HSM.Structs;
using _Project.Scripts.Core;
using _Project.Scripts.Utilities;
using _Project.Scripts.Utilities.HSM;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.HSM {
    public class Chase : State {
        private readonly PlayerDetector _playerDetector;
        private readonly CountdownTimer _timer;
        private readonly NavMeshAgent _agent;
        private readonly ChaseDeps _chaseDeps;
        private bool _forceStop;
        private Animator _animator;
        public Chase(StateMachine stateMachine, State parent, NavMeshAgent agent, ChaseDeps chaseDeps, PlayerDetector playerDetector, Animator animator) : base(stateMachine, parent) {
            _playerDetector = playerDetector;
            _chaseDeps = chaseDeps;
            _timer = new CountdownTimer(chaseDeps.maxTime);
            _agent = agent;
            _animator = animator;
        }

        protected override void OnEnter() {
            _timer.Start();
            _agent.isStopped = false;
            _forceStop = false;
            _animator.SetTrigger(EnemyRoot.WalkHash);
            _agent.ResetPath(); 
        }

        protected override void OnUpdate(float deltaTime) {
            if (_timer.IsRunning) {
                _timer.Tick(deltaTime);
            }

            var target = SceneServiceLocator.Current.Player.PlayerFacade;
            if (target == null) {
                _forceStop = true;
                return;
            }
            if (NavMesh.SamplePosition(target.Root.position, out NavMeshHit hit, _chaseDeps.maxDistance, NavMesh.AllAreas)) {
                _agent.SetDestination(hit.position);
                _animator.SetFloat(EnemyRoot.VelocityHash, _agent.velocity.normalized.magnitude);
            }
            else {
                _forceStop = true;
            }

        }

        protected override void OnExit() {
            _timer.Stop();
            _forceStop = false;
        }
        
        protected override State GetTransition() {
            var root = (EnemyRoot)Parent;
            if (_playerDetector.CanAttackPlayer())
                return root.Combat;
            return _timer.IsFinished || _forceStop ? root.Wander : null;
        }
    }
}
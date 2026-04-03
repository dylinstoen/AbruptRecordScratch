using _Project.Scripts.Combat.BaseEnemy;
using _Project.Scripts.Combat.HSM.Motors;
using _Project.Scripts.Combat.HSM.Structs;
using _Project.Scripts.Utilities.HSM;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.HSM {
    public class Wander : State {
        private readonly PlayerDetector _playerDetector;
        private readonly WanderMotor _wanderMotor;
        private readonly NavMeshAgent _agent;
        public Wander(StateMachine stateMachine, State parent, WanderDeps wanderDeps, NavMeshAgent agent, PlayerDetector playerDetector, Animator animator) : base(stateMachine, parent) {
            _playerDetector = playerDetector;
            _agent = agent;
            _wanderMotor = new WanderMotor(wanderDeps, agent, animator);
        }

        protected override void OnEnter() {
            _wanderMotor.OnEnter();
            _agent.isStopped = false;
            _agent.ResetPath();
        }

        protected override void OnUpdate(float deltaTime) {
            _wanderMotor.Update(deltaTime);
        }

        protected override void OnExit() {
            _wanderMotor.Exit();
        }

        protected override State GetTransition() {
            var root = (EnemyRoot)Parent;
            var canDetect = _playerDetector.CanDetectPlayer();
            var canAttack = _playerDetector.CanAttackPlayer();
            return canDetect switch {
                true when canAttack => root.Combat,
                true => root.Chase,
                _ => null
            };
        }
    }
}
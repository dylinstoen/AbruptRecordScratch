using _Project.Scripts.Combat.BaseEnemy;
using _Project.Scripts.Combat.HSM.Structs;
using _Project.Scripts.Utilities.HSM;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.HSM {
    public class EnemyRoot : State {
        public readonly Combat Combat;
        public readonly Chase Chase;
        public readonly Wander Wander;
        private PlayerDetector _playerDetector;

        public EnemyRoot(StateMachine stateMachine, Transform source, AttackDeps attackDeps, 
            RepositionDeps repositionDeps, WanderDeps wanderDeps, ChaseDeps chaseDeps, NavMeshAgent agent, PlayerDetector playerDetector) : base(stateMachine, null) {
            Combat = new Combat(stateMachine, this, source, attackDeps, repositionDeps, agent, playerDetector);
            Chase = new Chase(stateMachine, this, agent, chaseDeps, playerDetector);
            Wander = new Wander(stateMachine, this, wanderDeps, agent, playerDetector);
            _playerDetector = playerDetector;
        }

        protected override State GetInitialState() => Wander;

        protected override State GetTransition() => !_playerDetector.PlayerExist() ? Wander : null;
    }
}
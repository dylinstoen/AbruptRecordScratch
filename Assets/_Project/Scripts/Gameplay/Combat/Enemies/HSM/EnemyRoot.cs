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
        private readonly PlayerDetector _playerDetector;
        
        public static readonly int IdleHash = Animator.StringToHash("Idle");
        public static readonly int AttackHash = Animator.StringToHash("Attack");
        public static readonly int WalkHash = Animator.StringToHash("Walk");
        public static readonly int DieHash = Animator.StringToHash("Die");
        public static readonly int VelocityHash = Animator.StringToHash("Velocity");

        public EnemyRoot(StateMachine stateMachine, Transform source, AttackDeps attackDeps, 
            RepositionDeps repositionDeps, WanderDeps wanderDeps, ChaseDeps chaseDeps, NavMeshAgent agent, PlayerDetector playerDetector, Animator animator) : base(stateMachine, null) {
            Combat = new Combat(stateMachine, this, source, attackDeps, repositionDeps, agent, playerDetector, animator);
            Chase = new Chase(stateMachine, this, agent, chaseDeps, playerDetector, animator);
            Wander = new Wander(stateMachine, this, wanderDeps, agent, playerDetector, animator);
            _playerDetector = playerDetector;
        }

        protected override State GetInitialState() => Wander;

        protected override State GetTransition() => !_playerDetector.PlayerExist() ? Wander : null;
    }
}
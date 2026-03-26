using _Project.Scripts.Combat.BaseEnemy;
using _Project.Scripts.Utilities.HSM;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.HSM {
    public class Combat : State {
        public Attack Attack;
        public Reposition Reposition;
        private PlayerDetector _playerDetector;

        public Combat(StateMachine stateMachine, State parent, Transform source, AttackDeps attackDeps, 
            RepositionDeps repositionDeps, NavMeshAgent agent, PlayerDetector playerDetector) : base(stateMachine, parent) {
            Attack = new Attack(stateMachine, this, source, agent, attackDeps, playerDetector);
            Reposition =  new Reposition(stateMachine, this, source, repositionDeps, agent, playerDetector);
            _playerDetector = playerDetector;
        }

        protected override State GetInitialState() => Attack;

        protected override State GetTransition() =>
            !_playerDetector.CanAttackPlayer() ? ((EnemyRoot)Parent).Chase : null;
    }
}
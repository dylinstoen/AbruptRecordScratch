using System;
using System.Linq;
using _Project.Scripts.Combat.BaseEnemy;
using _Project.Scripts.Combat.HSM.Structs;
using UnityEngine;
using _Project.Scripts.Utilities.HSM;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.HSM {
    public class EnemyStateDriver : MonoBehaviour {
        [SerializeField] private AttackDeps attackDeps;
        [SerializeField] private RepositionDeps repositionDeps;
        [SerializeField] private WanderDeps wanderDeps;
        [SerializeField] private ChaseDeps chaseDeps;
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private PlayerDetector playerDetector;
        
        private string lastPath;
        private StateMachine machine;
        State root;

        private void Awake() {
            // StateMachine stateMachine, Transform source, AttackDeps attackDeps, RepositionDeps repositionDeps, WanderDeps wanderDeps, ChaseDeps chaseDeps, NavMeshAgent agent, PlayerDetector playerDetector
            root = new EnemyRoot(null, transform, attackDeps, repositionDeps, wanderDeps, chaseDeps, navMeshAgent, playerDetector); 
            machine = new StateMachine(root);
            var builder = new StateMachineBuilder(root);
            machine = builder.Build();
        }

        private void Update() {
            machine.Tick(Time.deltaTime);
            var path = StatePath(machine.Root.Leaf());
            if (lastPath != null && path != lastPath) {
                lastPath = path;
            }
        }

        static string StatePath(State s) {
            return string.Join(" > ", s.PathToRoot().AsEnumerable().Reverse().Select(n => n.GetType().Name));
        }
    }
}
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
            
        }

        private void Update() {
            machine.Tick(Time.deltaTime);
            var path = StatePath(machine.Root.Leaf());
            if (path != lastPath) {
                Debug.Log("State: " + path);
                lastPath = path;
            }
        }

        static string StatePath(State s) {
            return string.Join(" > ", s.PathToRoot().AsEnumerable().Reverse().Select(n => n.GetType().Name));
        }
    }
}
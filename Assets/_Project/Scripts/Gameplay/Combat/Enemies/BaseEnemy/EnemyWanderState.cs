using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.BaseEnemy {
    public class EnemyWanderState : EnemyBaseState {
        private float _wanderRadius;
        private NavMeshAgent _agent;
        private Vector3 _startPosition;
        NavMeshPath _path;
        public EnemyWanderState(Enemy enemy, Animator animator, float wanderRadius, NavMeshAgent agent) : base(enemy, animator) {
            _wanderRadius = wanderRadius;
            _agent = agent;
            _startPosition = agent.transform.position;
        }

        public override void OnEnter() {
            base.OnEnter();
        }
        

        public override void Update() {
            base.Update();
            if (HasReachedDestination()) {
                Vector3 randomOffset = Random.insideUnitSphere *  _wanderRadius;
                randomOffset.y = 0;
                Vector3 position = randomOffset + _startPosition;
                if(NavMesh.SamplePosition(position, out NavMeshHit hit, _wanderRadius, NavMesh.AllAreas)) {
                    _agent.SetDestination(hit.position);
                }
            }
        }

        private bool HasReachedDestination() {
            return !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance && (_agent.hasPath || _agent.velocity.sqrMagnitude == 0f);
        }

        public override void OnExit() {
            
        }
    }
}
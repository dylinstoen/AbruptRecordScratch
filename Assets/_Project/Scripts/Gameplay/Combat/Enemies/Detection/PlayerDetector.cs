using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Core;
using _Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Combat.BaseEnemy {
    public class PlayerDetector : MonoBehaviour {
        [SerializeField] private float detectionCooldown = 1f;
        [SerializeField] private float detectionAngle = 60f;
        [SerializeField] private float detectionRadius = 10f;
        [SerializeField] private float innerDetectionRadius = 5f;
        [SerializeField] private float attackRange = 2f;
        [SerializeField] private LayerMask attackLayerMask;
        private CountdownTimer _detectionTimer;
        private IDetectionStrategy _detectionStrategy;
        

        private void Start() {
            _detectionTimer = new CountdownTimer(detectionCooldown);
            _detectionStrategy = new ConeDetectionStrategy(detectionAngle, detectionRadius, innerDetectionRadius);
        }
        
        public bool PlayerExist() => SceneServiceLocator.Current.Player.PlayerFacade != null;
        
        public bool CanDetectPlayer() {
            Transform target = SceneServiceLocator.Current.Player.PlayerFacade.Root;
            if (!target)
                return false;
            bool detectPlayer = _detectionStrategy.Execute(target, transform, _detectionTimer);
            return _detectionTimer.IsRunning || detectPlayer;
        }
        public bool CanAttackPlayer() {
            Transform target = SceneServiceLocator.Current.Player.PlayerFacade.Root;
            if (!target)
                return false;
            var directionToPlayer = target.position - transform.position;
            return Physics.Raycast(transform.position, directionToPlayer, attackRange, attackLayerMask, QueryTriggerInteraction.Ignore);
        }
        public void SetDetectionStrategy(IDetectionStrategy detectionStrategy) => _detectionStrategy = detectionStrategy;


        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, innerDetectionRadius);
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position + transform.forward * attackRange);
        }
    }
}
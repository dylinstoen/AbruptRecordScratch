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
        private CountdownTimer _detectionTimer;
        private IDetectionStrategy _detectionStrategy;

        private void Start() {
            _detectionTimer = new CountdownTimer(detectionCooldown);
            _detectionStrategy = new ConeDetectionStrategy(detectionAngle, detectionRadius, innerDetectionRadius);
        }
        
        public bool CanDetectPlayer() {
            bool isDead = SceneServiceLocator.Current.Player.IsDead;
            bool isRunning = _detectionTimer.IsRunning;
            bool detectPlayer = _detectionStrategy.Execute(SceneServiceLocator.Current.Player.PlayerFacade.Root,
                transform, _detectionTimer);
            return !isDead && (isRunning || detectPlayer);
        }
        public bool CanAttackPlayer() {
            var directionToPlayer = SceneServiceLocator.Current.Player.PlayerFacade.Root.position - transform.position;
            return directionToPlayer.magnitude <= attackRange;
        }
        public void SetDetectionStrategy(IDetectionStrategy detectionStrategy) => _detectionStrategy = detectionStrategy;


        private void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, detectionRadius);
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, innerDetectionRadius);
            
        }
    }
}
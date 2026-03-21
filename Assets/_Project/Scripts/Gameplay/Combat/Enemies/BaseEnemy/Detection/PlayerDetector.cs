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
        public Transform Player { get; private set; }
        private IPlayerService _playerService;

        private void Awake() {
            _playerService = SceneServiceLocator.Current.Player;
            Player = _playerService.PlayerFacade.Root;
        }

        private void Start() {
            _detectionTimer = new CountdownTimer(detectionCooldown);
            _detectionStrategy = new ConeDetectionStrategy(detectionAngle, detectionRadius, innerDetectionRadius);
        }
        public bool CanDetectPlayer() {
            return !(_playerService.IsDead) && (_detectionTimer.IsRunning || _detectionStrategy.Execute(Player, transform, _detectionTimer));
        }
        public bool CanAttackPlayer() {
            var directionToPlayer = Player.position - transform.position;
            return directionToPlayer.magnitude <= attackRange;
        }
        public void SetDetectionStrategy(IDetectionStrategy detectionStrategy) => _detectionStrategy = detectionStrategy;
    }
}
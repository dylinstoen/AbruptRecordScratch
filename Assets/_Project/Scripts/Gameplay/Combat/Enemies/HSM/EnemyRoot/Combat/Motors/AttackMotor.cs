using System.Collections;
using _Project.Scripts.Combat.Weapon;
using _Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Combat.HSM {
    public class AttackMotor {
        public bool IsRunning { get; private set; }
        public bool IsFinished => !IsRunning;
        
        private readonly int _shotCount;
        private readonly float _rotationSpeed;
        private readonly float _maxAttackAngle;
        
        private Transform _target;
        private readonly Transform _source;
        private int _shotsRemaining;
        private EnemyWeaponController  _controller;
        
        public AttackMotor(AttackDeps attackDeps, Transform source, Animator animator) {
            _shotCount = attackDeps.shotCount;
            _rotationSpeed = attackDeps.rotationSpeed;
            _maxAttackAngle = attackDeps.maxAttackAngle;
            _source = source;
            _controller = attackDeps.enemyWeaponController;
        }

        public void Update(float deltaTime) {
            if (!IsRunning || !_target)
                return;
            RotateTowardsTarget();
            if (!IsFacingTarget())
                return;
            if (!_controller.CanAttack()) {
                return;
            }
            if (_shotsRemaining <= 0) {
                StopAttack();
                return;
            }
            FireShot();
            _shotsRemaining--;
        }

        void FireShot() {
            _controller.StartFire();
        } 

        public void StopAttack() {
            IsRunning = false;
            _target = null;
            _controller.StopFire();
            _shotsRemaining = 0;
        }

        private void RotateTowardsTarget() {
            Vector3 directionToTarget = _target.position - _source.position;
            directionToTarget.y = 0;
            if (directionToTarget.sqrMagnitude < 0.0001f)
                return;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
            _source.rotation = Quaternion.RotateTowards(_source.rotation, targetRotation, _rotationSpeed * Time.deltaTime * 360f);
        }

        private bool IsFacingTarget() {
            Vector3 directionToTarget = _target.position - _source.position;
            directionToTarget.y = 0;
            if (directionToTarget.sqrMagnitude < 0.0001f)
                return true;
            float angle = Vector3.Angle(_source.forward, directionToTarget.normalized);
            return angle <= _maxAttackAngle;
        }

        public void BeginAttck(Transform target) {
            if (!target || _shotCount <= 0)
                return;
            
            _target = target;
            _shotsRemaining = _shotCount;
            IsRunning = true;
        }
    }
}
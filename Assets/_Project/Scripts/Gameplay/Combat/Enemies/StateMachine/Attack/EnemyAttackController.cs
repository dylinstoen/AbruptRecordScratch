using System;
using UnityEngine;
using _Project.Scripts.Utilities;
namespace _Project.Scripts.Combat.BaseEnemy.Attack {
    public class EnemyAttackController : MonoBehaviour {
        public bool IsRunning { get; private set; }
        public bool IsFinished => !IsRunning;
        
        [SerializeField] private int shotCount = 1;
        [SerializeField] private float attackCoolDown = 1f;
        [SerializeField] private float rotationSpeed = 10f;
        [SerializeField] private float maxAttackAngle = 5f;
        
        private CountdownTimer _cooldownTimer;
        private Transform _target;
        private int _shotsRemaining;
        
        private void Awake() {
            _cooldownTimer = new CountdownTimer(attackCoolDown);
        }

        private void Update() {
            if (!IsRunning || !_target)
                return;
            RotateTowardsTarget();
            if (!IsFacingTarget())
                return;
            if (_cooldownTimer.IsRunning) {
                _cooldownTimer.Tick(Time.deltaTime);
                return;
            }
            FireShot();
            _shotsRemaining--;
            if (_shotsRemaining <= 0) {
                StopAttack();
                return;
            }
            _cooldownTimer.Start();
        }

        public void StopAttack() {
            IsRunning = false;
            _target = null;
            _shotsRemaining = 0;
            _cooldownTimer.Stop();
        }

        private void FireShot() {
            Debug.Log("Shot Fired");
        }

        private void RotateTowardsTarget() {
            Vector3 directionToTarget = _target.position - transform.position;
            directionToTarget.y = 0;
            if (directionToTarget.sqrMagnitude < 0.0001f)
                return;
            Quaternion targetRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime * 360f);
        }

        private bool IsFacingTarget() {
            Vector3 directionToTarget = _target.position - transform.position;
            directionToTarget.y = 0;
            if (directionToTarget.sqrMagnitude < 0.0001f)
                return true;
            float angle = Vector3.Angle(transform.forward, directionToTarget.normalized);
            return angle <= maxAttackAngle;
        }

        public void BeginAttck(Transform target) {
            if (!target || shotCount <= 0)
                return;
            _target = target;
            _shotsRemaining = shotCount;
            IsRunning = true;
            _cooldownTimer.Stop();
        }
    }
}
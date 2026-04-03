using System;
using System.Collections;
using _Project.Scripts.Combat.HSM;
using _Project.Scripts.Utilities;
using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts.Combat.Weapon {
    public class EnemyMeleeController : EnemyWeaponController {
        private enum AttackState {
            Idle,
            Windup,
            Active,
            Cooldown
        }
        public static int AttackToHash = Animator.StringToHash("Attack");
        [Header("Melee Controller")]
        [SerializeField, Child(Flag.IncludeInactive | Flag.ExcludeSelf)] private Collider meleeCollider;
        [SerializeField, Self] private Animator enemyAnimator;
        private CountdownTimer _stateTimer;
        public float colliderDelay = 0.2f;
        private bool _wantsToFire;
        private AttackState _state = AttackState.Idle;
        
        

        public void OnValidate() {
            this.ValidateRefs();
        }

        public void Start() {
            _stateTimer = new CountdownTimer(colliderDelay);
            _state = AttackState.Idle;
        }

        public void Update() {
            switch (_state) {
                case AttackState.Idle:
                    if (_wantsToFire)
                        BeginWindup();
                    break;
                case AttackState.Windup:
                    _stateTimer?.Tick(Time.deltaTime);
                    if (_stateTimer is { IsFinished: true })
                        BeginActive();
                    break;
                case AttackState.Active:
                    _stateTimer?.Tick(Time.deltaTime);
                    if (_stateTimer is { IsFinished: true })
                        BeginCooldown();
                    break;
                case AttackState.Cooldown:
                    _stateTimer?.Tick(Time.deltaTime);
                    if (_stateTimer is { IsFinished: true })
                        EndCooldown();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        private void BeginWindup() {
            _state = AttackState.Windup;
            animator.SetTrigger(EnemyRoot.AttackHash);
            meleeCollider.enabled = false;
            enemyAnimator.SetTrigger(AttackToHash);
            _stateTimer.Reset(colliderDelay);
            _stateTimer.Start();
        }
        
        private void BeginActive() {
            _state = AttackState.Active;
            meleeCollider.enabled = true;
            Debug.Log("firing");
            _stateTimer.Reset(attackTime);
            _stateTimer.Start();
        }
        
        private void BeginCooldown() {
            _state = AttackState.Cooldown;
            meleeCollider.enabled = false;
            _stateTimer.Reset(coolDown);
            _stateTimer.Start();
        }
        
        private void EndCooldown() {
            _state = AttackState.Idle;
        }

        public override void StartFire() {
            _wantsToFire = true;
        }

        public override void StopFire() {
            _wantsToFire = false;
        }

        public override bool CanAttack() => _state == AttackState.Idle;
    }
}
using _Project.Scripts.Combat.HSM;
using _Project.Scripts.Combat.Weapon;
using _Project.Scripts.Utilities;
using Assets._Project.Scripts.Gameplay.Combat.Enemies;
using KBCore.Refs;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyRangedController : EnemyWeaponController {
    private enum AttackState {
        Idle,
        Windup,
        Active,
        Cooldown
    }
    public static int AttackToHash = Animator.StringToHash("Attack");
    [Header("Ranged Controller")]
    [SerializeField] private EnemyProjectile enemyProjectile;
    [SerializeField, Self] private Animator enemyAnimator;
    private CountdownTimer _stateTimer;
    public float projectileDelay = 0.2f;
    private bool _wantsToFire;
    private AttackState _state = AttackState.Idle;



    public void OnValidate() {
        this.ValidateRefs();
    }

    public void Start() {
        _stateTimer = new CountdownTimer(projectileDelay);
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
        enemyAnimator.SetTrigger(AttackToHash);
        _stateTimer.Reset(projectileDelay);
        _stateTimer.Start();
    }

    private void BeginActive() {
        _state = AttackState.Active;
        enemyProjectile.Fire();
        _stateTimer.Reset(attackTime);
        _stateTimer.Start();
    }

    private void BeginCooldown() {
        _state = AttackState.Cooldown;
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

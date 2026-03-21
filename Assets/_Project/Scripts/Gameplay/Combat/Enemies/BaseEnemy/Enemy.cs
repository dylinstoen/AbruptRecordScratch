using System;
using _Project.Scripts.Core;
using _Project.Scripts.Utilities;
using _Project.Scripts.Utilities.StateMachine;
using _Project.Scripts.Utilities.StateMachine.Interfaces;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.BaseEnemy {
    public class Enemy : MonoBehaviour {
        [SerializeField, Self] NavMeshAgent agent;
        [SerializeField, Child] Animator animator;
        [SerializeField] private PlayerDetector playerDetector;
        
        [SerializeField] private float wanderRadius = 5f;
        [SerializeField] private float timeBetweenAttacks = 1f;
        
        private StateMachine _stateMachine;
        private CountdownTimer _attackTimer;
        

        private void OnValidate() => this.ValidateRefs();

        private void Start() {
            _attackTimer = new  CountdownTimer(timeBetweenAttacks);
            
            _stateMachine = new StateMachine();
            
            EnemyWanderState wanderState = new EnemyWanderState(this, animator, wanderRadius, agent);
            EnemyAttackState attackState = new EnemyAttackState(this, animator, agent);
            EnemyRepositionState repositionState = new EnemyRepositionState(this, animator);
            //At(wanderState, attackState, new FuncPredicate(() => playerDetector.CanDetectPlayer()));
            //At(attackState, repositionState, new FuncPredicate(() => !playerDetector.CanAttackPlayer() || attackState.FinishedAttacking()));
            //At(repositionState, attackState, new FuncPredicate(() => playerDetector.CanAttackPlayer()));
            
            Any(wanderState, new FuncPredicate(() => !playerDetector.CanDetectPlayer()));
            _stateMachine.SetState(wanderState);
        }
        void At(IState from, IState to, IPredicate condition) => _stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => _stateMachine.AddAnyTransition(to, condition);

        private void Update() {
            _stateMachine.Update();
        }

        private void FixedUpdate() {
            _stateMachine.FixedUpdate();
        }

        public void Attack() {
            // TODO: Look at player
            // TODO: Fire weapon x times
            
        }
    }
}
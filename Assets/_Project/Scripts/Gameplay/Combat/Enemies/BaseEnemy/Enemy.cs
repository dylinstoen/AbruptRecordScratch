using System;
using _Project.Scripts.Utilities.StateMachine;
using _Project.Scripts.Utilities.StateMachine.Interfaces;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.AI;

namespace _Project.Scripts.Combat.BaseEnemy {
    public class Enemy : MonoBehaviour {
        [SerializeField, Self] NavMeshAgent agent;
        [SerializeField, Child] Animator animator;
        
        StateMachine stateMachine;

        private void OnValidate() => this.ValidateRefs();

        private void Start() {
            stateMachine = new StateMachine();
        }
        void At(IState from, IState to, IPredicate condition) => stateMachine.AddTransition(from, to, condition);
        void Any(IState to, IPredicate condition) => stateMachine.AddAnyTransition(to, condition);

        private void Update() {
            stateMachine.Update();
        }

        private void FixedUpdate() {
            stateMachine.FixedUpdate();
        }
    }
}
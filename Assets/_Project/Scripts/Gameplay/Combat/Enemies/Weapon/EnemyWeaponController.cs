using System;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Combat.Weapon {
    public abstract class EnemyWeaponController : MonoBehaviour {
        [Header("Weapon Controller")]
        public SourceVisualImpactProfileSO sourceVisual;
        public SourceAudioImpactProfileSO audioVisual;
        public Animator animator;
        public float damage = 10f;
        public float attackTime = 2f;
        public float coolDown = 1f;

        public abstract void StartFire();
        public abstract void StopFire();
        
        public abstract bool CanAttack();

    }
}
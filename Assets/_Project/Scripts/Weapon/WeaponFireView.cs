using System;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public class WeaponFireView : MonoBehaviour {
        private static readonly int IsFiring = Animator.StringToHash("IsFiring");

        [Header("Recoil")]
        [SerializeField] private Animator recoilAnimator;
        private IFireMode _fireMode;
        
        public void Initialize(IFireMode fireMode, float fireRate) {
            _fireMode = fireMode;
            _fireMode.ShotFired += ShotFired;
            _fireMode.DryFired += DryFired;
        }

        private void DryFired() {
            
        }

        public void ShotFired(RecoilSO recoil) {
            recoilAnimator.SetTrigger(IsFiring);
        }

        public void LateTick(in WeaponUseContext ctx) {
            
        }

        private void OnDestroy() {
            if (_fireMode == null) return;
            _fireMode.ShotFired -= ShotFired;
        }
    }
}
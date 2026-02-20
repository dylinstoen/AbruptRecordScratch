using System;
using _Project.Scripts.Weapon.Enums;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public class FullAutoFireMode : IFireMode {
        public event Action<FireAttempt, float> FireAttempted;
        private readonly float _fireRate;
        private bool _firing;
        private float _coolDownRemaining;
        private readonly IEmitterMode _emitterMode;
        private readonly IWeaponMagazine _weaponMagazine;
        private readonly int _costPerShot;
        
        public FullAutoFireMode(IWeaponMagazine weaponMagazine, IEmitterMode emitter, float fireRate, int costPerShot) {
            _fireRate = 1f/fireRate;
            _emitterMode = emitter;
            _weaponMagazine = weaponMagazine;
            _costPerShot = costPerShot;
        }
        

        public void OnEquip() {
            _firing = false;
            _coolDownRemaining = 0f;
        }

        public void StartFire(WeaponUseContext ctx) {
            _firing = true;
        }

        public void StopFire(WeaponUseContext ctx) {
            _firing = false;
        }

        public void Tick(WeaponUseContext ctx) {
            if (!_firing) return;
            if (_coolDownRemaining > 0f) {
                _coolDownRemaining -= ctx.DeltaTime;
                if (_coolDownRemaining > 0f) return;
            }
            while (_coolDownRemaining <= 0f) {
                if (!_weaponMagazine.TryConsumeAmmo(_costPerShot)) {
                    FireAttempted?.Invoke(FireAttempt.Empty, ctx.DeltaTime);
                    return;
                }
                FireAttempted?.Invoke(FireAttempt.Fired, ctx.DeltaTime);
                _emitterMode.Fire(ctx);
                _coolDownRemaining = _fireRate; 
            }
        }
    }
}
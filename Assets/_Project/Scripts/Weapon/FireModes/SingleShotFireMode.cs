using System;
using _Project.Scripts.Weapon.Enums;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public sealed class SingleShotFireMode: IFireMode {
        public event Action<FireAttempt, float> FireAttempted;
        private readonly IEmitterMode _emitterMode;
        private readonly float _coolDown;
        private readonly IWeaponMagazine _weaponMagazine;
        private readonly int _costPerShot;
        private float _coolDownRemaining;
        public SingleShotFireMode(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode, float coolDown, int costPerShot) {
            _emitterMode = emitterMode;
            _coolDown = coolDown;
            _weaponMagazine = weaponMagazine;
            _costPerShot = costPerShot;
        }
        
        public void Equip() { }

        public void StartFire(WeaponUseContext ctx) {
            if (_coolDownRemaining > 0f) return;
            if (!_weaponMagazine.TryConsumeAmmo(_costPerShot)) {
                FireAttempted.Invoke(FireAttempt.Empty, ctx.DeltaTime);
                return;
            }
            FireAttempted?.Invoke(FireAttempt.Fired, ctx.DeltaTime);
            _emitterMode.Fire(ctx);
            _coolDownRemaining = _coolDown;
        }

        public void StopFire(WeaponUseContext ctx) { }

        public void Tick(WeaponUseContext ctx) {
            if (_coolDownRemaining > 0f) {
                _coolDownRemaining -= ctx.DeltaTime;
            }
        }
    }
}
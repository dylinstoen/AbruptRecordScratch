using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public class FullAutoFireMode : IFireMode {
        private float _fireRate;
        private WeaponUseContext _lastCtx;
        private bool _firing;
        private float _nextTimeToFire;
        private IEmitterMode _emitterMode;
        private IWeaponAmmo _weaponAmmo;
        
        public FullAutoFireMode(IWeaponAmmo weaponAmmo, IEmitterMode emitter, float fireRate) {
            _fireRate = 1f/fireRate;
            _emitterMode = emitter;
            _weaponAmmo = weaponAmmo;
        }

        public void OnEquip() {
            _firing = false;
            _nextTimeToFire = 0f;
        }

        public void StartFire(WeaponUseContext ctx) {
            _firing = true;
        }

        public void StopFire(WeaponUseContext ctx) {
            _firing = false;
        }

        public void Tick(WeaponUseContext ctx) {
            if (!_firing) return;
            if (Time.time < _nextTimeToFire) return;
            _emitterMode.Fire(ctx);
            _nextTimeToFire = Time.time + _fireRate;
        }
    }
}
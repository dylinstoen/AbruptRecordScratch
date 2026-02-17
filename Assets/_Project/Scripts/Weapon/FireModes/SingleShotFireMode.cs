using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public sealed class SingleShotFireMode: IFireMode {
        private IEmitterMode _emitterMode;
        private float _coolDown;
        private float _nextTimeToFire;
        private IWeaponAmmo _weaponAmmo;
        public SingleShotFireMode(IWeaponAmmo weaponAmmo, IEmitterMode emitterMode, float coolDown) {
            _emitterMode = emitterMode;
            _coolDown = coolDown;
            _weaponAmmo = weaponAmmo;
        }
        public void OnEquip() { }

        public void StartFire(WeaponUseContext ctx) {
            if (Time.time < _nextTimeToFire) 
                return;
            _emitterMode.Fire(ctx);
            _nextTimeToFire = Time.time + _coolDown;
        }

        public void StopFire(WeaponUseContext ctx) { }

        public void Tick(WeaponUseContext ctx) { }
    }
}
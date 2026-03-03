using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Static;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public sealed class SingleShotFireMode: IFireMode {
        public event Action DryFired;
        public event Action<RecoilSO> ShotFired;
        private readonly IEmitterMode _emitterMode;
        private readonly float _fireRate;
        private readonly IWeaponMagazine _weaponMagazine;
        private readonly int _costPerShot;
        private float _coolDownRemaining;
        private RecoilSO _recoilConfig;
        private ICameraRecoilService _cameraRecoilService;
        public SingleShotFireMode(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode, float fireRate, int costPerShot, RecoilSO recoilConfig, ICameraRecoilService cameraRecoilService) {
            _emitterMode = emitterMode;
            _fireRate = 1f/fireRate;
            _weaponMagazine = weaponMagazine;
            _costPerShot = costPerShot;
            _recoilConfig = recoilConfig;
            _cameraRecoilService = cameraRecoilService;
        }
        
        public void Equip() { }

        public void StartFire(WeaponUseContext ctx) {
            if (_coolDownRemaining > 0f) return;
            if (!_weaponMagazine.TryConsumeAmmo(_costPerShot)) {
                DryFired?.Invoke();
                _cameraRecoilService.ResetRecoil();
                return;
            }
            _cameraRecoilService.OnShotFired();
            ShotFired?.Invoke(_recoilConfig);
            _emitterMode.Fire(ctx);
            _coolDownRemaining = _fireRate;
        }

        public void Unequip() { }

        public void StopFire(WeaponUseContext ctx) {
            _cameraRecoilService.OnTriggerReleased(false);
        }

        public void Tick(WeaponUseContext ctx) {
            if (_coolDownRemaining > 0f) {
                _coolDownRemaining -= ctx.DeltaTime;
            }
        }
    }
}
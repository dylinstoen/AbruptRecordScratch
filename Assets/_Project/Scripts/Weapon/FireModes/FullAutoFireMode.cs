using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Static;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public class FullAutoFireMode : IFireMode {
        public event Action DryFired;
        public event Action<RecoilSO> ShotFired;

        private readonly float _fireRate;
        private bool _firing;
        private float _coolDownRemaining;
        private readonly IEmitterMode _emitterMode;
        private readonly IWeaponMagazine _weaponMagazine;
        private readonly int _costPerShot;
        private RecoilSO _recoilConfig;
        private ICameraRecoilService _cameraRecoilService;
        
        public FullAutoFireMode(IWeaponMagazine weaponMagazine, IEmitterMode emitter, float fireRate, int costPerShot, RecoilSO recoilConfig, ICameraRecoilService cameraRecoilService) {
            _fireRate = 1f/fireRate;
            _emitterMode = emitter;
            _weaponMagazine = weaponMagazine;
            _costPerShot = costPerShot;
            _recoilConfig = recoilConfig;
            _cameraRecoilService = cameraRecoilService;
        }
        

        public void Equip() {
            _firing = false;
            _coolDownRemaining = 0f;
        }

        public void Unequip() { }

        public void StartFire(WeaponUseContext ctx) {
            _firing = true;
        }

        public void StopFire(WeaponUseContext ctx) {
            _firing = false;
            _cameraRecoilService.OnTriggerReleased();
        }

        public void Tick(WeaponUseContext ctx) {
            if (!_firing) return;
            if (_coolDownRemaining > 0f) {
                _coolDownRemaining -= ctx.DeltaTime;
                if (_coolDownRemaining > 0f) return;
            }
            while (_coolDownRemaining <= 0f) {
                if (!_weaponMagazine.TryConsumeAmmo(_costPerShot)) {
                    DryFired?.Invoke();
                    return;
                }
                _cameraRecoilService.OnShotFired();
                ShotFired?.Invoke(_recoilConfig);
                _emitterMode.Fire(ctx);
                _coolDownRemaining = _fireRate; 
            }
        }
    }
}
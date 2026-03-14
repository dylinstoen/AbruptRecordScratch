using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Weapon.Static;
using _Project.Scripts.Weapon.Struct;
using _Project.Scripts.Weapon.Stucts;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public sealed class SingleShotFireMode: IFireMode {
        public event Action DryFired;
        public event Action ShotFired;
        private readonly IEmitterMode _emitterMode;
        private readonly float _fireRate;
        private readonly IWeaponMagazine _weaponMagazine;
        private readonly int _costPerShot;
        private float _coolDownRemaining;
        private  CinemachineImpulseSource _impulseSource;
        private RecoilProfile _recoilProfile;
        private float _spread = 0.2f;
        private IAudioService _audioService;
        private AudioCue _audioCue;
        public SingleShotFireMode(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode, IAudioService audioService, AudioCue audioCue, float fireRate, int costPerShot, float spread) {
            _emitterMode = emitterMode;
            _fireRate = 1f/fireRate;
            _weaponMagazine = weaponMagazine;
            _costPerShot = costPerShot;
            _spread = spread;
            _audioService = audioService;
            _audioCue = audioCue;
        }
        public SingleShotFireMode(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode, IAudioService audioService, AudioCue audioCue, float fireRate, int costPerShot, float spread, CinemachineImpulseSource impulseSource, RecoilProfile recoilProfile) {
            _emitterMode = emitterMode;
            _fireRate = 1f/fireRate;
            _weaponMagazine = weaponMagazine;
            _costPerShot = costPerShot;
            _impulseSource = impulseSource;
            _recoilProfile = recoilProfile;
            _spread = spread;
            _audioService = audioService;
            _audioCue = audioCue;
        }
        public void Equip() { }

        public void StartFire(WeaponUseContext ctx) {
            if (_coolDownRemaining > 0f) return;
            if (!_weaponMagazine.TryConsumeAmmo(_costPerShot)) {
                DryFired?.Invoke();
                return;
            }
            if (_impulseSource) {
                Vector3 vel = GenerateRandomVelocity(_recoilProfile.velXRange, _recoilProfile.velYRange, _recoilProfile.velZRange);
                _impulseSource?.GenerateImpulseAtPositionWithVelocity(_impulseSource.transform.position, vel * _recoilProfile.force);
            }
            ShotFired?.Invoke();
            _audioService.Play3D(ctx.AimRay.origin, Quaternion.LookRotation(ctx.AimRay.direction), _audioCue);
            _emitterMode.Fire(GenerateNewRay(ctx.AimRay));
            _coolDownRemaining = _fireRate;
        }
        private Ray GenerateNewRay(Ray ray) {
            Vector2 randomPoint = UnityEngine.Random.insideUnitCircle * _spread;
            Vector3 offset = new Vector3(randomPoint.x, randomPoint.y, 0f);
            Ray newRay =  new Ray(ray.origin, ray.direction + offset);
            return newRay;
        }
        public Vector3 GenerateRandomVelocity(Vector2 xRange, Vector2 yRange, Vector2 zRange) {
            float x = UnityEngine.Random.Range(xRange.x, xRange.y);
            float y = UnityEngine.Random.Range(yRange.x, yRange.y);
            float z = UnityEngine.Random.Range(zRange.x, zRange.y);
            return new Vector3(x, y, z);
        }
        public void Unequip() { }

        public void StopFire(WeaponUseContext ctx) { }

        public void Tick(WeaponUseContext ctx) {
            if (_coolDownRemaining > 0f) {
                _coolDownRemaining -= ctx.DeltaTime;
            }
        }
    }
}
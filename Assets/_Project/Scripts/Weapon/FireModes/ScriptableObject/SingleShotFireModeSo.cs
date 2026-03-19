using _Project.Scripts.Actors;
using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Weapon.Struct;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "SingleShot", menuName = "Weapon/FireMode/SingleShot")]
    public class SingleShotFireModeSo : FireModeSO {
        public override IFireMode Create(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode, IAudioService audioService, AudioCue audioCue, int costPerShot, float fireRate, float spread) {
            return new SingleShotFireMode(weaponMagazine, emitterMode, audioService, audioCue, fireRate, costPerShot, spread);
        }

        public override IFireMode Create(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode, IAudioService audioService, AudioCue audioCue, int costPerShot, float fireRate, float spread, CinemachineImpulseSource impulseSource, RecoilProfile recoilProfile) {
            return new SingleShotFireMode(weaponMagazine, emitterMode, audioService, audioCue, fireRate, costPerShot, spread, impulseSource, recoilProfile);
        }
    }
}
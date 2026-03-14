using _Project.Scripts.Actors;
using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Weapon.Struct;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public abstract class FireModeSO : ScriptableObject {
        public abstract IFireMode Create(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode, IAudioService audioService, AudioCue audioCue, int costPerShot, float fireRate, float spread);
        public abstract IFireMode Create(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode, IAudioService audioService, AudioCue audioCue, int costPerShot, float fireRate, float spread, CinemachineImpulseSource impulseSource, RecoilProfile recoilProfile);
    }
}
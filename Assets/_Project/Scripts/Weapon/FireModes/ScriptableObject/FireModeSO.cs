using _Project.Scripts.Actors;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public abstract class FireModeSO : ScriptableObject {
        public abstract IFireMode Create(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode, int costPerShot, float fireRate, RecoilSO recoilConfig, ICameraRecoilService cameraRecoilService);
    }
}
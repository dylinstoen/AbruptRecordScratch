using _Project.Scripts.Actors;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "SingleShot", menuName = "Weapon/FireMode/SingleShot")]
    public class SingleShotFireModeSo : FireModeSO {
        public override IFireMode Create(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode, int costPerShot, float fireRate, RecoilSO recoilConfig, ICameraRecoilService cameraRecoilService) {
            return new SingleShotFireMode(weaponMagazine, emitterMode, fireRate, costPerShot > 0 ? costPerShot : 1, recoilConfig, cameraRecoilService);
        }
    }
}
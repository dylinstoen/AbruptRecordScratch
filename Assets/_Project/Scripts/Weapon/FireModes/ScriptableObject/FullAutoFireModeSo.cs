using _Project.Scripts.Actors;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "Full Auto", menuName = "Weapon/FireMode/Full Auto")]
    public class FullAutoFireModeSo : FireModeSO {
        public override IFireMode Create(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode,  int costPerShot, float fireRate, RecoilSO recoilConfig, ICameraRecoilService cameraRecoilService) {
            return new FullAutoFireMode(weaponMagazine, emitterMode, fireRate, costPerShot > 0 ? costPerShot : 1, recoilConfig, cameraRecoilService);
        }
    }
}
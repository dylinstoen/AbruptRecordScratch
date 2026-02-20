using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "SingleShot", menuName = "Weapon/FireMode/SingleShot")]
    public class SingleShotFireModeSo : FireModeSO {
        [SerializeField] private float coolDown;
        public override IFireMode Create(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode, int costPerShot) {
            return new SingleShotFireMode(weaponMagazine, emitterMode, coolDown, costPerShot > 0 ? costPerShot : 1);
        }
    }
}
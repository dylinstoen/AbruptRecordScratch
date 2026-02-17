using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "SingleShot", menuName = "Weapon/FireMode/SingleShot")]
    public class SingleShotFireModeSo : FireModeSO {
        [SerializeField] private float coolDown;
        public override IFireMode Create(IWeaponAmmo weaponAmmo, IEmitterMode emitterMode) {
            return new SingleShotFireMode(weaponAmmo, emitterMode, coolDown);
        }
    }
}
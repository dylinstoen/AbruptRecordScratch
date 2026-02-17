using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "Full Auto", menuName = "Weapon/FireMode/Full Auto")]
    public class FullAutoFireModeSo : FireModeSO {
        [SerializeField] private float fireRate;
        public override IFireMode Create(IWeaponAmmo weaponAmmo, IEmitterMode emitterMode) {
            return new FullAutoFireMode(weaponAmmo, emitterMode, fireRate > 0f ? fireRate : 1f);
        }
    }
}
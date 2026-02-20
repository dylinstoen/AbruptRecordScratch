using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "Full Auto", menuName = "Weapon/FireMode/Full Auto")]
    public class FullAutoFireModeSo : FireModeSO {
        [SerializeField] private float fireRate;
#if UNITY_EDITOR
        private void OnValidate()
        {
            if (fireRate < 0.01f) fireRate = 0.01f;
        }
#endif
        public override IFireMode Create(IWeaponMagazine weaponMagazine, IEmitterMode emitterMode,  int costPerShot) {
            return new FullAutoFireMode(weaponMagazine, emitterMode, fireRate, costPerShot > 0 ? costPerShot : 1);
        }
    }
}
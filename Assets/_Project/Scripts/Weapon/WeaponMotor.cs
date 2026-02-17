using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public sealed class WeaponMotor : MonoBehaviour {
        IFireMode _fireMode;

        public void Initialize(IFireMode fireMode) {
            _fireMode = fireMode;
            fireMode.OnEquip();
        }

        public void Tick(in WeaponUseContext ctx) => _fireMode.Tick(ctx);
        public void StartFire(in WeaponUseContext ctx) => _fireMode.StartFire(ctx);
        public void StopFire(in WeaponUseContext ctx) => _fireMode.StopFire(ctx);
        public void SetActive(bool active) => gameObject.SetActive(active);
    }
}


using System;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public sealed class WeaponMotor : MonoBehaviour {
        private IWeaponStateController _weaponStateController;
        // [SerializeField] private float distance = 1f;
        // [SerializeField] private float radius = 0.5f;
        public void Initialize(IWeaponStateController weaponStateController) {
            _weaponStateController = weaponStateController;
        }

        private WeaponUseContext _ctx;
        public void Tick(in WeaponUseContext ctx) {
            _ctx = ctx;
            _weaponStateController.Tick(ctx);
        }

        public void StartFire(in WeaponUseContext ctx) => _weaponStateController.StartFire(ctx);
        public void StopFire(in WeaponUseContext ctx) => _weaponStateController.StopFire(ctx);
        public void RequestReload(in WeaponUseContext ctx) => _weaponStateController.RequestReload();
        public void FirstEquipped() => _weaponStateController.FirstEquipped();
        public void Equip() => _weaponStateController.Equip();
        public void Unequip() => _weaponStateController.Unequip();

        public void Dispose() => _weaponStateController.Dispose();

        public void SetActive(bool active) => gameObject.SetActive(active);

        // private void OnDrawGizmos() {
        //     Gizmos.color = Color.red;
        //     Vector3 origin = _ctx.AimRay.origin + (_ctx.AimRay.direction * distance);
        //     Gizmos.DrawSphere(origin, radius);
        // }
    }
}


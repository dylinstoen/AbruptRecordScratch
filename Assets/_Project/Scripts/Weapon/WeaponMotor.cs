using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public sealed class WeaponMotor : MonoBehaviour {
        private IWeaponStateController _weaponStateController;
        private IAmmoInventory _inventory;
        public void Initialize(IWeaponStateController weaponStateController) {
            _weaponStateController = weaponStateController;
        }
        public void Tick(in WeaponUseContext ctx) => _weaponStateController.Tick(ctx);
        public void StartFire(in WeaponUseContext ctx) => _weaponStateController.StartFire(ctx);
        public void StopFire(in WeaponUseContext ctx) => _weaponStateController.StopFire(ctx);
        public void RequestReload(in WeaponUseContext ctx) => _weaponStateController.RequestReload();
        public void FirstEquipped() => _weaponStateController.FirstEquipped();
        public void Equip() => _weaponStateController.Equip();
        public void Unequip() => _weaponStateController.Unequip();

        public void Dispose() => _weaponStateController.Dispose();

        public void SetActive(bool active) => gameObject.SetActive(active);
        
    }
}


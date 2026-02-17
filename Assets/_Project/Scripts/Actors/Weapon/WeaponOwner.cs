using System;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Actors.Weapon;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class WeaponOwner : MonoBehaviour {
        
        private WeaponRunner _weaponRunner;
        private bool _activated = false;

        public void Activate(WeaponDeps weaponDeps, WeaponLoadoutSO weaponLoadoutSo, AmmoProfileSO ammoProfileSo, IIntentSource intent) {
            if(_activated) 
                throw new InvalidOperationException("Already bounded");
            
            var ammoInventory = new AmmoInventory();
            foreach (var startingAmmo in ammoProfileSo.Entries) {
                ammoInventory.SetMax(startingAmmo.AmmoType, startingAmmo.MaxReserve);
                ammoInventory.Add(startingAmmo.AmmoType, startingAmmo.StartingReserve);
            }
            
            var weaponInventory = new WeaponInventory();
            IWeaponFactory weaponFactory = new WeaponFactory();
            foreach (var weapon in weaponLoadoutSo.Entries) {
                var currentWeapon = weaponFactory.Create(weapon, ammoInventory, weaponDeps);
                weaponInventory.Equip(currentWeapon);
            }
            _weaponRunner = new WeaponRunner(intent, weaponDeps.AimRaySource, weaponInventory, ammoInventory);
            _activated = true;
        }

        private void Update() {
            if (!_activated) return;
            _weaponRunner.Tick(Time.deltaTime);
        }

        private void LateUpdate() {
            if (!_activated) return;
            _weaponRunner.LateTick(Time.deltaTime);
        }
    }
}
using System;
using _Project.Input;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Actors.Weapon;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class WeaponOwner : MonoBehaviour {
        [SerializeField] private WeaponHudPresenter weaponHudPresenter;
        [SerializeField] private WeaponInventory weaponInventory;
        private WeaponRunner _weaponRunner;
        private bool _builtRunner = false;
        private bool _builtWeapons = false;

        public void BuildWeapons(WeaponDeps weaponDeps, WeaponLoadoutSO weaponLoadoutSo) {
            if(_builtWeapons) 
                throw new InvalidOperationException("Weapons have already been built");
            IWeaponFactory weaponFactory = new WeaponFactory();
            foreach (var weapon in weaponLoadoutSo.Entries) {
                var currentWeapon = weaponFactory.Create(weapon, weaponDeps);
                weaponInventory.Equip(currentWeapon);
            }
            _builtWeapons = true;
        }
        public void BuildRunner(IIntentSource intent, IAimRaySource aimRaySource) {
            if(_builtRunner) 
                throw new InvalidOperationException("Already bounded");
            _weaponRunner = new WeaponRunner(intent, aimRaySource, weaponInventory);
            _builtRunner = true;
        }

        private void Update() {
            if (!_builtRunner || !_builtWeapons) return;
            _weaponRunner.Tick(Time.deltaTime);
        }

        private void LateUpdate() {
            if (!_builtRunner || !_builtWeapons) return;
            _weaponRunner.LateTick(Time.deltaTime);
        }
    }
}
using System;
using _Project.Input;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Actors.Weapon;
using _Project.Scripts.Input;
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
                if (!weaponInventory.TryEquip(currentWeapon)) {
                    Debug.LogWarning("Duplicate weapon ID " + currentWeapon.Identity.ID + " in weapon loadout SO. Disposing it...");
                    currentWeapon.Disposable.Dispose();
                }
                currentWeapon.Logic.OnCreate();
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
            if (!_builtRunner) {
                Debug.LogError("No runner has been built in weapon owner and its trying to run");
                return;
            }

            if (!_builtWeapons) {
                Debug.LogError("No weapon has been built in weapon owner and its trying to run");
                return;
            }
            _weaponRunner.Tick(Time.deltaTime);
        }

        private void LateUpdate() {
            if (!_builtRunner) {
                Debug.LogError("No runner has been built in weapon owner and its trying to run");
                return;
            }

            if (!_builtWeapons) {
                Debug.LogError("No weapon has been built in weapon owner and its trying to run");
                return;
            }
            _weaponRunner.LateTick(Time.deltaTime);
        }

    }
}
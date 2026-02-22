using System;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Actors.Weapon;
using _Project.Scripts.Input;
using _Project.Scripts.UI.Weapon;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Enums;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class WeaponOwner : MonoBehaviour, IWeaponAcquirer, IAmmoAcquirer {
        [SerializeField] private WeaponInventory weaponInventory;
        [SerializeField] private AmmoInventory ammoInventory;
        private WeaponHudPresenter _weaponHudPresenter;
        private WeaponRunner _weaponRunner;
        private WeaponDeps _weaponDeps;
        private IWeaponFactory _weaponFactory;
        private bool _builtRunner = false;
        private bool _builtWeapons = false;
        
        public bool TryAddWeapon(WeaponSO weaponSo) {
            if (weaponInventory.ContainsWeapon(weaponSo)) return false;
            var weapon = _weaponFactory.Create(weaponSo, _weaponDeps);
            if (!weaponInventory.TryEquip(weapon)) {
                Debug.LogWarning("Duplicate weapon ID " + weapon.Identity.ID + " in weapon loadout SO. Disposing it...");
                weapon.Disposable.Dispose();
            }
            return true;
        }
        
        public bool TryAddAmmo(AmmoType ammoType, int ammoToAdd) {
            if (ammoInventory.StoreUpToMax(ammoType, ammoToAdd) > 0) return true;
            return false;
        }

        public void BuildWeaponHud(IWeaponHud weaponHud) {
            _weaponHudPresenter = new WeaponHudPresenter(weaponInventory, weaponHud);
        }

        private void OnDestroy() {
            _weaponHudPresenter.Dispose();
        }

        public void BuildAmmo(AmmoProfileSO ammoProfileSO) {
            ammoInventory.BuildAmmo(ammoProfileSO);
        }
        
        public void BuildWeapons(Transform weaponLogicMount, Transform weaponViewMount, WeaponLoadoutSO weaponLoadoutSo) {
            if(_builtWeapons) 
                throw new InvalidOperationException("Weapons have already been built");
            var weaponDeps = BuildWeaponDependencies(weaponLogicMount, weaponViewMount);
            IWeaponFactory weaponFactory = new WeaponFactory();
            foreach (var weapon in weaponLoadoutSo.Entries) {
                var currentWeapon = weaponFactory.Create(weapon, weaponDeps);
                if (!weaponInventory.TryEquip(currentWeapon)) {
                    Debug.LogWarning("Duplicate weapon ID " + currentWeapon.Identity.ID + " in weapon loadout SO. Disposing it...");
                    currentWeapon.Disposable.Dispose();
                }
            }
            _weaponFactory = weaponFactory;
            _weaponDeps = weaponDeps;
            _builtWeapons = true;
        }

        private WeaponDeps BuildWeaponDependencies(Transform weaponLogicMount, Transform weaponViewMount) {
            return new WeaponDeps {WeaponLogicMount = weaponLogicMount, WeaponViewMount = weaponViewMount, AmmoInventory = ammoInventory};
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
using System;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Input;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Enums;
using UnityEngine;

namespace _Project.Scripts.Actors {
    [RequireComponent(typeof(WeaponInventory))]
    [RequireComponent(typeof(AmmoInventory))]
    public class WeaponOwner : MonoBehaviour, IWeaponAcquirer, IAmmoAcquirer {
        [SerializeField] private Transform weaponLogicMount;
        private WeaponInventory _weaponInventory;
        private AmmoInventory _ammoInventory;
        private WeaponRunner _weaponRunner;
        private WeaponDeps _weaponDeps;
        private IWeaponFactory _weaponFactory;
        private IIntentSource _intent;
        private bool _builtRunner = false;
        private bool _builtWeapons = false;
        
        private void Awake() {
            _weaponInventory = GetComponent<WeaponInventory>();
            _ammoInventory = GetComponent<AmmoInventory>();
        }
        
        public bool TryAddWeapon(WeaponSO weaponSo) {
            if (_weaponInventory.ContainsWeapon(weaponSo)) return false;
            var weapon = _weaponFactory.Create(weaponSo, _weaponDeps);
            if (!_weaponInventory.TryEquip(weapon)) {
                Debug.LogWarning("Duplicate weapon ID " + weapon.Identity.ID + " in weapon loadout SO. Disposing it...");
                weapon.Disposable.Dispose();
            }
            return true;
        }
        
        public bool TryAddAmmo(AmmoType ammoType, int ammoToAdd) {
            if (_ammoInventory.StoreUpToMax(ammoType, ammoToAdd) > 0) return true;
            return false;
        }

        public void BindIntent(IIntentSource intent) {
            _intent = intent;
        }
        
        public void BuildWeapons(Transform weaponViewMount, WeaponLoadoutSO weaponLoadoutSo) {
            if(_builtWeapons) 
                throw new InvalidOperationException("Weapons have already been built");
            var weaponDeps = new WeaponDeps {WeaponLogicMount = weaponLogicMount, WeaponViewMount = weaponViewMount, AmmoInventory = _ammoInventory};
            IWeaponFactory weaponFactory = new WeaponFactory();
            foreach (var weapon in weaponLoadoutSo.Entries) {
                var currentWeapon = weaponFactory.Create(weapon, weaponDeps);
                if (!_weaponInventory.TryEquip(currentWeapon)) {
                    Debug.LogWarning("Duplicate weapon ID " + currentWeapon.Identity.ID + " in weapon loadout SO. Disposing it...");
                    currentWeapon.Disposable.Dispose();
                }
            }
            _weaponFactory = weaponFactory;
            _weaponDeps = weaponDeps;
            _builtWeapons = true;
        }
        
        public void BuildRunner(IAimRaySource aimRaySource) {
            if(_builtRunner) 
                throw new InvalidOperationException("Already bounded");
            _weaponRunner = new WeaponRunner(_intent, aimRaySource, _weaponInventory);
            _builtRunner = true;
        }

        private void Update() {
            if (!_builtWeapons || !_builtRunner) return;
            _weaponRunner.Tick(Time.deltaTime);
        }

        private void LateUpdate() {
            if (!_builtWeapons || !_builtRunner) return;
            _weaponRunner.LateTick(Time.deltaTime);
        }
    }
}
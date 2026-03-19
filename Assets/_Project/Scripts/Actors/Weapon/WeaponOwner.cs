using System;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Combat;
using _Project.Scripts.Gameplay;
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
        private bool _initialized = false;
        
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

        public void Initialize(IIntentSource intent, IImpactService impactService, Transform weaponViewMount, WeaponLoadoutSO weaponLoadoutSo, IAimRaySource aimRaySource, Transform reticleMount, IAudioService audioService) {
            if (_initialized) return;
            _intent = intent;
            BuildWeapons(impactService, weaponViewMount, weaponLoadoutSo, reticleMount, audioService);
            _weaponRunner = new WeaponRunner(_intent, aimRaySource, _weaponInventory);
            _initialized = true;
        }
        
        private void BuildWeapons(IImpactService impactService, Transform weaponViewMount, WeaponLoadoutSO weaponLoadoutSo, Transform reticleMount, IAudioService audioService) {
            var weaponDeps = new WeaponDeps {
                WeaponLogicMount = weaponLogicMount,
                WeaponViewMount = weaponViewMount,
                AmmoInventory = _ammoInventory,
                Owner = gameObject,
                ReticleMount = reticleMount,
                ImpactService = impactService,
                AudioService = audioService
            };
            IWeaponFactory weaponFactory = new WeaponFactory();
            if (weaponLoadoutSo.Entries.Count > 0) {
                foreach (var weapon in weaponLoadoutSo.Entries) {
                    var currentWeapon = weaponFactory.Create(weapon, weaponDeps);
                    if (!_weaponInventory.TryEquip(currentWeapon)) {
                        Debug.LogWarning("Duplicate weapon ID " + currentWeapon.Identity.ID + " in weapon loadout SO. Disposing it...");
                        currentWeapon.Disposable.Dispose();
                    }
                } 
            }
            var secondaryWeapon = weaponFactory.Create(weaponLoadoutSo.DefaultWeapon, weaponDeps);
            if (!_weaponInventory.TryEquipDefault(secondaryWeapon)) {
                secondaryWeapon.Disposable.Dispose();
            }
            _weaponFactory = weaponFactory;
            _weaponDeps = weaponDeps;
        }

        private void Update() {
            if (!_initialized) return;
            _weaponRunner.Tick(Time.deltaTime);
        }

        private void LateUpdate() {
            if (!_initialized) return;
            _weaponRunner.LateTick(Time.deltaTime);
        }
    }
}
using System;
using System.Collections.Generic;
using _Project.Scripts.Input;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Stucts;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Actors {
    // Rep Inv: Switch weapons assigning the current selected weapon
    public sealed class WeaponInventory : MonoBehaviour, IDisposable {
        private readonly List<WeaponFacets> _weapons = new();

        private WeaponFacets _defaultWeapon;
        public WeaponFacets DefaultWeapon => _defaultWeapon;

        public List<WeaponFacets> Weapons => _weapons;
        private readonly Dictionary<string, WeaponFacets> _weaponDictionary = new();
        public event Action<WeaponFacets> OnWeaponChanged;
        public WeaponFacets CurrentWeapon { get; private set; }
        private int _currentIndex = -1;
        
        public void NextWeapon() {
            if (_weapons.Count == 0) {
                return;
            }
            int nextIndex = (_currentIndex + 1) % _weapons.Count;
            SwitchWeapon(nextIndex);
        }

        public void PreviousWeapon() {
            if (_weapons.Count == 0) {
                return;
            }
            int prevIndex = (_currentIndex - 1 + _weapons.Count) % _weapons.Count;
            SwitchWeapon(prevIndex);
        }

        private void SwitchWeapon(int index) {
            if (_weapons == null || index < 0 || index >= _weapons.Count || _weapons.Count == 0 || index == _currentIndex)
                return;
            CurrentWeapon.Equipable?.Unequip();
            _currentIndex = index;
            CurrentWeapon = _weapons[_currentIndex];
            CurrentWeapon.Equipable?.Equip();
            OnWeaponChanged?.Invoke(CurrentWeapon);
        }

        public bool ContainsWeapon(WeaponSO weaponSo) {
            if (_weaponDictionary.ContainsKey(weaponSo.iD)) {
                return true;
            }
            return false;
        }
        public bool TryEquip(WeaponFacets weapon) {
            if(_weaponDictionary.ContainsKey(weapon.Identity.ID))
                return false;
            _weapons.Add(weapon);
            weapon.Equipable?.FirstEquipped();
            SwitchWeapon(_weapons.Count - 1);
            _weaponDictionary.Add(weapon.Identity.ID, weapon);
            return true;
        }

        public bool TryEquipDefault(WeaponFacets weapon) {
            _defaultWeapon = weapon;
            weapon.Equipable?.FirstEquipped();
            weapon.Equipable?.Equip();
            if (_weapons.Count == 0) {
                OnWeaponChanged?.Invoke(_defaultWeapon);
            }
            return true;
        }

        private void OnDestroy() {
            Dispose();
        }
        

        public void Dispose() {
            foreach (var weapon in _weapons) {
                weapon.Disposable.Dispose();
            }
        }
    }

}

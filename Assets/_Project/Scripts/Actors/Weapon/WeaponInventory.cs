using System;
using System.Collections.Generic;
using _Project.Input;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Stucts;
using JetBrains.Annotations;
using UnityEngine;

namespace _Project.Scripts.Actors {
    // Rep Inv: Switch weaponst assigning the current selected weapon
    public sealed class WeaponInventory : MonoBehaviour, IWeaponInventory {
        private readonly List<WeaponFacets> _weapons = new();
        private readonly Dictionary<string, WeaponFacets> _weaponDictionary = new();
        public event Action<WeaponFacets> OnWeaponChanged;
        public WeaponFacets CurrentWeapon { get; private set; }
        private int _currentIndex = -1;
        
        public void NextWeapon() {
            int nextIndex = (_currentIndex + 1) % _weapons.Count;
            SwitchWeapon(nextIndex);
        }

        public void PreviousWeapon() {
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

        public bool TryEquip(WeaponFacets weapon) {
            if(_weaponDictionary.ContainsKey(weapon.Identity.ID))
                return false;
            _weapons.Add(weapon);
            weapon.Equipable?.Equip();
            SwitchWeapon(_weapons.Count - 1);
            _weaponDictionary.Add(weapon.Identity.ID, weapon);
            return true;
        }
        
        private void OnDestroy() {
            foreach (var weapon in _weapons) {
                weapon.Disposable.Dispose();
            }
        }
    }

}

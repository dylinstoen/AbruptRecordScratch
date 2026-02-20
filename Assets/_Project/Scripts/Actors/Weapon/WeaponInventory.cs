using System;
using System.Collections.Generic;
using _Project.Input;
using _Project.Scripts.Weapon;
using UnityEngine;

namespace _Project.Scripts.Actors {
    // Rep Inv: Switch weaponst assigning the current selected weapon
    public sealed class WeaponInventory : MonoBehaviour, IWeaponInventory {
        private readonly List<IWeapon> _weapons = new();
        public event Action<IWeaponAmmoView> OnWeaponChanged;
        public IWeapon CurrentWeapon { get; private set; }
        private int _currentIndex = 0;
        
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
            CurrentWeapon?.OnUnequip();
            CurrentWeapon?.SetActive(false);
            _currentIndex = index;
            CurrentWeapon = _weapons[_currentIndex];
            CurrentWeapon.OnEquip();
            CurrentWeapon.SetActive(true);
            OnWeaponChanged?.Invoke(CurrentWeapon);
        }

        public void Equip(IWeapon weapon) {
            _weapons.Add(weapon);
            weapon.SetActive(false);
            SwitchWeapon(_weapons.Count - 1);
        }
    }

}

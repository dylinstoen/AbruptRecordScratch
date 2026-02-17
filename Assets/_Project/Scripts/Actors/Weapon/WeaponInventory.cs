using System;
using System.Collections.Generic;
using _Project.Input;
using _Project.Scripts.Weapon;
using UnityEngine;

namespace _Project.Scripts.Actors {
    // Rep Inv: Switch weaponst assigning the current selected weapon
    public sealed class WeaponInventory {
        private readonly List<IWeapon> _weapons = new();
        public IWeapon CurrentWeapon { get; private set; }
        private int _currentIndex = 0;
        
        public void NextWeapon() {
            if (_weapons == null || _weapons.Count == 0) {
                return;
            }
            int nextIndex = (_currentIndex + 1) % _weapons.Count;
            SwitchWeapon(nextIndex);
        }

        public void PreviousWeapon() {
            if (_weapons == null || _weapons.Count == 0) {
                return;
            }
            int prevIndex = (_currentIndex - 1 + _weapons.Count) % _weapons.Count;
            SwitchWeapon(prevIndex);
        }

        public void SwitchWeapon(int index) {
            if (index <= 0 && index >= _weapons.Count) {
                return;
            }
            CurrentWeapon?.SetActive(false);
            _currentIndex = index;
            CurrentWeapon = _weapons[_currentIndex];
            CurrentWeapon.SetActive(true);
        }

        public void Equip(IWeapon weapon) {
            _weapons.Add(weapon);
            weapon.SetActive(false);
            SwitchWeapon(_weapons.Count - 1);
        }
    }

}

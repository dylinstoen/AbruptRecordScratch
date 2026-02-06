using System;
using System.Collections.Generic;
using FPS.Weapon;
using UnityEngine;

namespace FPS.Character {
    // Rep Inv: Switch weaponst assigning the current selected weapon
    public class WeaponInventory : MonoBehaviour {
        [SerializeField] private List<FPS.Weapon.Weapon> weapons;
        private FPS.Weapon.Weapon currentWeapon;
        public FPS.Weapon.Weapon CurrentWeapon => currentWeapon;
        private int currentIndex = 0;

        private void Start() {
            if (weapons == null || weapons.Count == 0) {
                return;
            }

            for (int i = 0; i < weapons.Count; i++) {
                weapons[i].gameObject.SetActive(false);
            }

            SwitchWeapon(0);
        }

        public void NextWeapon() {
            if (weapons == null || weapons.Count == 0) {
                return;
            }
            int nextIndex = (currentIndex + 1) % weapons.Count;
            SwitchWeapon(nextIndex);
        }

        public void PreviousWeapon() {
            if (weapons == null || weapons.Count == 0) {
                return;
            }
            int prevIndex = (currentIndex - 1 + weapons.Count) % weapons.Count;
            SwitchWeapon(prevIndex);
        }

        private void SwitchWeapon(int index) {
            if (index <= 0 && index >= weapons.Count) {
                return;
            }
            if (currentWeapon != null)
                currentWeapon.gameObject.SetActive(false);
            currentIndex = index;
            currentWeapon = weapons[currentIndex];
            currentWeapon.gameObject.SetActive(true);
        }
        
        public void AddWeapon(FPS.Weapon.Weapon weapon)
        {
            weapons.Add(weapon);
            weapon.gameObject.SetActive(false);
            SwitchWeapon(weapons.Count - 1); // Switch to newly added weapon
        }
    }

}

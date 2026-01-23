using System;
using System.Collections.Generic;
using UnityEngine;

namespace FPS.WeaponSystem {
    public class Inventory : MonoBehaviour {
        IWeaponInputReader inputReader;
        public Weapon currentWeapon;
        public Transform weaponTransform;

        private void Start() {
            Equip(currentWeapon);
        }

        public void Equip(Weapon weapon) {
            //weapon.transform.position = weaponTransform.position;
        }
    }
}


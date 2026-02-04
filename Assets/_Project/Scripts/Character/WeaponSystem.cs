using System;
using FPS.Input;
using FPS.Weapon;
using UnityEngine;

namespace FPS.Character {
    // Rep Inv: Handle firing selected weapon from WeaponSystem
    public class WeaponSystem : MonoBehaviour {
        [SerializeField] private WeaponInventory weaponInventory;
        private WeaponContext weaponContext;

        public void Inject(WeaponContext weaponContext) {
            this.weaponContext = weaponContext;
        }
        
        private void Update() {
            weaponInventory.CurrentWeapon.ProcessInput(weaponContext, Time.deltaTime);
            float switchWeaponInput = weaponContext.fireInput.SwitchWeapon();
            if (switchWeaponInput > 0.5f) {
                weaponInventory.NextWeapon();
            }
            if (switchWeaponInput < -0.5f) {
                weaponInventory.PreviousWeapon();
            }
        }
    }
}

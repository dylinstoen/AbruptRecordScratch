using System;
using FPS.Input;
using UnityEngine;

namespace FPS.Character {
    // Rep Inv: Handle firing selected weapon from WeaponSystem
    public class WeaponSystem : MonoBehaviour {
        private IFireInput input;
        [SerializeField] private WeaponInventory weaponInventory;
        public void Inject(IFireInput input) {
            this.input = input;
        }
        private void Update() {
            weaponInventory.CurrentWeapon.ProcessInput(input.PrimaryFire(), Time.deltaTime);
            float switchWeaponInput = input.SwitchWeapon();
            if (switchWeaponInput > 0.5f) {
                weaponInventory.NextWeapon();
            }
            if (switchWeaponInput < -0.5f) {
                weaponInventory.PreviousWeapon();
            }
        }
    }
}

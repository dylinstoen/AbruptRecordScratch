using System;
using FPS.Aiming;
using FPS.Input;
using FPS.Weapon;
using UnityEngine;

namespace FPS.Character {
    // Rep Inv: Handle firing selected weapon from WeaponSystem
    public class WeaponSystem : MonoBehaviour {
        [SerializeField] private WeaponInventory weaponInventory;
        private IAimSource aimSource;
        private IFireInput fireInput;

        public void Inject(IAimSource aimSource, IFireInput fireInput) {
            this.aimSource = aimSource;
            this.fireInput = fireInput;
        }
        
        private void Update() {
            WeaponSnapshot snapshot = new WeaponSnapshot(aimSource.Forward, fireInput.PrimaryFire(), Time.deltaTime);
            weaponInventory.CurrentWeapon.Tick(snapshot);
            HandleWeaponSwitch(fireInput.SwitchWeapon());
        }

        void HandleWeaponSwitch(float switchWeaponState) {
            switch (switchWeaponState) {
                case <= 0.5f and >= -0.5f:
                    return;
                case > 0.5f:
                    weaponInventory.NextWeapon();
                    break;
                default:
                    weaponInventory.PreviousWeapon();
                    break;
            }
        }
    }
}

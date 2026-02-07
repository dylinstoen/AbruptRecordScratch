using System;
using FPS.Aiming;
using FPS.Input;
using FPS.Weapon;
using FPS.Weapon.Stucts;
using UnityEngine;

namespace FPS.Character {
    // Rep Inv: Handle firing selected weapon or weapons from WeaponSystem
    // Maps input and direction to aim into the current weapon or weapons
    // Sends where its aiming to the current weapon or weapons visual controls
    public class WeaponSystem : MonoBehaviour {
        [SerializeField] private WeaponInventory weaponInventory;
        private IAimSource aimSource;
        private IFireInput fireInput;

        public void Init(IAimSource aimSource, IFireInput fireInput) {
            this.aimSource = aimSource;
            this.fireInput = fireInput;
        }
        
        public void Tick() {
            // If i want right click to fire i just change what i pass into tick
            weaponInventory.CurrentWeapon.Tick(fireInput.PrimaryFire(), aimSource.Forward);
            HandleWeaponSwitch(fireInput.SwitchWeapon());
        }

        public void LateTick() {
            weaponInventory.CurrentWeapon.LateTick(aimSource.Forward, aimSource.Position);
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

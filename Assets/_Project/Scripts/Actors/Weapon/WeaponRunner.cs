using System;
using _Project.Scripts.Input;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Project.Scripts.Actors {
    public sealed class WeaponRunner {
        private readonly WeaponInventory _weaponInventory;
        private readonly IIntentSource _intent;
        private readonly IAimRaySource _aimRaySource;
        private bool _primaryFireWasHeld = false;
        private bool _secondaryFireWasHeld = false;
        public WeaponRunner(IIntentSource intent, IAimRaySource aimRaySource, WeaponInventory weaponInventory) {
            _intent = intent;
            _weaponInventory = weaponInventory;
            _aimRaySource = aimRaySource;
        }

        public void Tick(float deltaTime) {
            HandleWeaponSwitch();
            var primaryWeapon = _weaponInventory.Weapons.Count == 0 ? _weaponInventory.DefaultWeapon.Logic : _weaponInventory.CurrentWeapon.Logic;
            var secondaryWeapon = _weaponInventory.DefaultWeapon.Logic;
            primaryWeapon?.Tick(CreateContext(deltaTime));
            secondaryWeapon?.Tick(CreateContext(deltaTime));
            var primaryFireHeld = _intent.Current.PrimaryFireHeld;
            var secondaryFireHeld = _intent.Current.SecondaryFireHeld;
            if (primaryFireHeld && !_primaryFireWasHeld) primaryWeapon?.StartFire(CreateContext(deltaTime));
            if (!primaryFireHeld && _primaryFireWasHeld) primaryWeapon?.StopFire(CreateContext(deltaTime));
            if(secondaryFireHeld && !_secondaryFireWasHeld) secondaryWeapon?.StartFire(CreateContext(deltaTime));
            if(!secondaryFireHeld && _secondaryFireWasHeld) secondaryWeapon?.StopFire(CreateContext(deltaTime));
            _primaryFireWasHeld = primaryFireHeld;
            _secondaryFireWasHeld = secondaryFireHeld;
        }

        public void LateTick(float deltaTime) {
            var weapon = _weaponInventory.CurrentWeapon.Logic;
            var secondaryWeapon = _weaponInventory.DefaultWeapon.Logic;
            weapon?.LateTick(CreateContext(deltaTime));
            secondaryWeapon?.LateTick(CreateContext(deltaTime));
        }

        private void HandleWeaponSwitch() {
            float delta = _intent.Current.SwitchDelta;
            if (delta is <= 0.5f and >= -0.5f)
                return;
            if (delta > 0.5f) _weaponInventory.NextWeapon();
            else _weaponInventory.PreviousWeapon();
   
        }
        private WeaponUseContext CreateContext(float deltaTime) {
            return new WeaponUseContext(_aimRaySource.GetAimRay(), deltaTime);
        }
    }
}
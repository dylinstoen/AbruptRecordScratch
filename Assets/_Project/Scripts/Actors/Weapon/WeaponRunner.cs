using System;
using _Project.Scripts.Input;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Project.Scripts.Actors {
    public sealed class WeaponRunner {
        private readonly IWeaponInventory _weaponInventory;
        private readonly IIntentSource _intent;
        private readonly IAimRaySource _aimRaySource;
        private bool _fireWasHeld = false;
        
        public WeaponRunner(IIntentSource intent, IAimRaySource aimRaySource, IWeaponInventory weaponInventory) {
            _intent = intent;
            _weaponInventory = weaponInventory;
            _aimRaySource = aimRaySource;
            
        }

        public void Tick(float deltaTime) {
            HandleWeaponSwitch();
            var weapon = _weaponInventory.CurrentWeapon.Logic;
            weapon?.Tick(CreateContext(deltaTime));
            var fireHeld = _intent.Current.FireHeld;
            if (fireHeld && !_fireWasHeld) weapon?.StartFire(CreateContext(deltaTime));
            if (!fireHeld && _fireWasHeld) weapon?.StopFire(CreateContext(deltaTime));
            _fireWasHeld = fireHeld;
        }

        public void LateTick(float deltaTime) {
            var weapon = _weaponInventory.CurrentWeapon.Logic;
            weapon?.LateTick(CreateContext(deltaTime));
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
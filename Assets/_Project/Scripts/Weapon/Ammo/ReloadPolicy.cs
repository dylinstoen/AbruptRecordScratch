using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Enums;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public sealed class ReloadPolicy : IReloadPolicy {
        public event Action<ReloadAttempt> ReloadAttempted;
        public event Action<float> ReloadStarted;
        public event Action ReloadStopped;
        private IAmmoInventory _inventory;
        private readonly IWeaponMagazine _magazine;
        private readonly WeaponReloadViewBridge _weaponReloadViewBridge;
        private readonly float _reloadDuration;
        private float _currentReloadTime;
        private bool _isReloading;
        private float _elapsed;
        public AmmoType AmmoType { get; }


        public ReloadPolicy(IAmmoInventory ammoInventory, IWeaponMagazine magazine, AmmoType ammoType, float reloadDuration) {
            _inventory = ammoInventory;
            _magazine = magazine;
            AmmoType = ammoType;
            _reloadDuration = reloadDuration;
        }

        public bool StartReloading() {
            if (_isReloading) return false;
            int missing = _magazine.MissingAmmo;
            if (missing <= 0) {
                ReloadAttempted?.Invoke(ReloadAttempt.Cancel);
                return false;
            }
            if (_inventory.GetCurrent(AmmoType) <= 0) {
                ReloadAttempted?.Invoke(ReloadAttempt.Cancel);
                return false;
            }
            _elapsed = 0f;
            _isReloading = true;

            ReloadStarted?.Invoke(_reloadDuration);
            return true;
        }

        public void QuickFill() {
            int missing = _magazine.MissingAmmo;
            if (missing <= 0) return;
            int take = _inventory.ConsumeUpTo(AmmoType, missing);
            if (take <= 0) return;
            int accepted = _magazine.LoadUpTo(take);
            int leftover = missing - accepted;
            if (leftover > 0) _inventory.StoreUpToMax(AmmoType, leftover);
        }

        private void CancelReloading() {
            if (!_isReloading) return;
            _elapsed = 0f;
            _isReloading = false;
            
            ReloadStopped?.Invoke();
            ReloadAttempted?.Invoke(ReloadAttempt.Cancel);
        }

        public void Tick(in WeaponUseContext ctx) {
            if(!_isReloading) return;
            _elapsed += ctx.DeltaTime;
            if (_elapsed < _reloadDuration) return;
            CompleteReloading();
        }        
        
        private void CompleteReloading() {
            if (!_isReloading) return;
            int missing = _magazine.MissingAmmo;
            int staged = _inventory.ConsumeUpTo(AmmoType, missing);
            int accepted = _magazine.LoadUpTo(staged);
            int leftover = staged - accepted;
            if(leftover > 0) _inventory.StoreUpToMax(AmmoType, leftover);
            Debug.Log("Reload Complete: pulled" + staged + " from ammo inventory. Leftover was " +  leftover);
            _elapsed = 0f;
            _isReloading = false;
            ReloadAttempted?.Invoke(ReloadAttempt.Complete);
            ReloadStopped?.Invoke();
        }
        public void OnUnequip() {
            CancelReloading();
        }
        public void OnEquip() {
            CancelReloading();
        }
    }
}
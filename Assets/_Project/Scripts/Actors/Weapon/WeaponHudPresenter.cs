using System;
using _Project.Scripts.UI;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Stucts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Actors {
    [RequireComponent(typeof(WeaponInventory))]
    public class WeaponHudPresenter : MonoBehaviour, IAmmoEvents, IDisposable {
        private WeaponInventory _weaponInventory;
        private IWeaponAmmoView _ammoView;
        public event Action<int, int> OnAmmoChanged;

        private void Awake() {
            _weaponInventory = GetComponent<WeaponInventory>();
            _weaponInventory.OnWeaponChanged += OnWeaponChanged;
        }
        

        private void OnWeaponChanged(WeaponFacets weapon) {
            if (_ammoView != null)
                _ammoView.AmmoChanged -= Refresh;
            _ammoView = weapon.AmmoView;
            _ammoView.AmmoChanged += Refresh;
            
            Refresh();
        }

        public void Refresh() {
            if (_ammoView == null) {
                OnAmmoChanged?.Invoke(0,0);
                return;
            }
            OnAmmoChanged?.Invoke(_ammoView.Mag, _ammoView.Reserve);
        }

        private void OnDestroy() {
            Dispose();
        }

        public void Dispose() {
            _weaponInventory.OnWeaponChanged -= OnWeaponChanged;
            if(_ammoView != null) _ammoView.AmmoChanged -= Refresh;
        }

        
    }
}
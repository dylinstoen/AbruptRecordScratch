using System;
using _Project.Scripts.UI.Weapon;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Stucts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Actors {
    public class WeaponHudPresenter : IDisposable {
        private readonly IWeaponInventory _weaponInventory;
        private readonly IWeaponHud _hud;
        private IWeaponAmmoView _ammoView;
        public WeaponHudPresenter(IWeaponInventory weaponInventory, IWeaponHud hud) {
            _weaponInventory = weaponInventory;
            _hud = hud;
            _weaponInventory.OnWeaponChanged += OnWeaponChanged;
        }

        private void OnWeaponChanged(WeaponFacets weapon) {
            if (_ammoView != null)
                _ammoView.AmmoChanged -= Refresh;
            _ammoView = weapon.AmmoView;
            _ammoView.AmmoChanged += Refresh;
            Refresh();
        }

        private void Refresh() {
            if (_ammoView == null) {
                _hud.SetAmmo(0,0);
                return;
            }
            _hud.SetAmmo(_ammoView.Mag, _ammoView.Reserve);
        }

        public void Dispose() {
            _weaponInventory.OnWeaponChanged -= OnWeaponChanged;
            if(_ammoView != null) _ammoView.AmmoChanged -= Refresh;
        }
    }
}
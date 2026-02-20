using System;
using _Project.Scripts.Weapon;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Actors {
    public class WeaponHudPresenter : MonoBehaviour {
        private TMP_Text _ammoText;
        private IWeaponMagazine _weaponMagazine;
        [SerializeField] private WeaponInventory weaponInventory;
        private IWeaponAmmoView _weaponAmmoView;

        private void Start() {

        }

        public void BindAmmoText(TMP_Text ammoText) {
            _ammoText = ammoText;
        }
        
        
        
        public void OnAmmoChange(IWeaponAmmoView weaponAmmoView) {
            _ammoText.text = weaponAmmoView.Mag.ToString() + "/" + weaponAmmoView.Reserve.ToString();
        }
        
    }
}
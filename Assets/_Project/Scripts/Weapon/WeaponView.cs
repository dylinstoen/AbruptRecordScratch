using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Actors.Structs;
using UnityEngine;
using _Project.Scripts.Weapon.Stucts;
namespace _Project.Scripts.Weapon {
    public sealed  class WeaponView : MonoBehaviour {
        [Header("Parts")]
        [SerializeField] private WeaponReloadView reloadView;
        [SerializeField] private WeaponAimView aimView;
        private WeaponReloadViewBridge _weaponReloadViewBridge;
        public void Dispose() {
            _weaponReloadViewBridge.Dispose();
        }

        public void Initialize(WeaponReloadViewBridge weaponReloadViewBridge) {
            _weaponReloadViewBridge = weaponReloadViewBridge;
            weaponReloadViewBridge.Initialize(reloadView);
        }
        
        public void LateTick(in WeaponUseContext ctx) {
            aimView.LateTick(ctx);
        }
        
        public void SetActive(bool active) => gameObject.SetActive(active);
        
        public void OnFired() {
            // TODO: Play gun fired sound effect, muzzle flash, and apply a visual recoil based on weapon data
            
        }
    }
}


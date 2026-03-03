using UnityEngine;
using _Project.Scripts.Weapon.Stucts;
namespace _Project.Scripts.Weapon {
    public sealed  class WeaponView : MonoBehaviour {
        [Header("Parts")]
        [SerializeField] private WeaponReloadView reloadView;
        [SerializeField] private WeaponFireView weaponFireView;
        [SerializeField] private WeaponAimView aimView;
        private WeaponReloadBridge _weaponReloadBridge;
        public void Dispose() {
            _weaponReloadBridge.Dispose();
        }

        public void Initialize(WeaponReloadBridge weaponReloadBridge, IFireMode fireMode, float coolDown) {
            _weaponReloadBridge = weaponReloadBridge;
            weaponReloadBridge.Initialize(reloadView);
            weaponFireView.Initialize(fireMode, coolDown);
        }
        
        public void LateTick(in WeaponUseContext ctx) {
            aimView.LateTick(ctx);
            weaponFireView.LateTick(ctx);
        }
        
        public void SetActive(bool active) => gameObject.SetActive(active);
    }
}


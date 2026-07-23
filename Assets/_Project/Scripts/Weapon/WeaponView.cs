using UnityEngine;
using _Project.Scripts.Weapon.Stucts;
using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay.Enums;
namespace _Project.Scripts.Weapon {
    public sealed  class WeaponView : MonoBehaviour {
        [Header("Parts")]
        [SerializeField] private WeaponReloadView reloadView;
        [SerializeField] private WeaponFireView weaponFireView;
        [SerializeField] private WeaponAimView aimView;
        private WeaponReloadBridge _weaponReloadBridge;
        private ILevelStateSource _levelStateSource;
        public void Dispose() {
            _weaponReloadBridge.Dispose();
        }

        public void OnStateChanged(LevelState newState) {
            if (reloadView == null)
                return;
            bool isPaused = newState == LevelState.Paused || newState == LevelState.Loading;
            reloadView.SetPause(isPaused);
        }

        public void Initialize(WeaponReloadBridge weaponReloadBridge, IFireMode fireMode, float coolDown, ILevelStateSource levelStateSource) {
            _weaponReloadBridge = weaponReloadBridge;
            _levelStateSource = levelStateSource;
            _levelStateSource.StateChanged += OnStateChanged;
            weaponReloadBridge.Initialize(reloadView);
            weaponFireView.Initialize(fireMode, coolDown);
            OnStateChanged(_levelStateSource.CurrentState);
        }
        
        public void LateTick(in WeaponUseContext ctx) {
            aimView.LateTick(ctx);
            weaponFireView.LateTick(ctx);
        }
        
        public void SetActive(bool active) => gameObject.SetActive(active);
    }
}


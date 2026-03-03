using System;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public sealed class WeaponReloadBridge {
        private WeaponReloadView _reloadView;
        private IReloadPolicy _policy;

        public void Initialize(WeaponReloadView reloadView) {
            _reloadView = reloadView;
        }
        public WeaponReloadBridge(IReloadPolicy policy) {
            _policy = policy;
            _policy.ReloadStarted += OnReloadStarted;
        }
        public void Dispose() {
            _policy.ReloadStarted -= OnReloadStarted;
        }
        public void OnReloadStarted(float duration) {
            _reloadView.PlayAnimation(duration);
        }
    }
}
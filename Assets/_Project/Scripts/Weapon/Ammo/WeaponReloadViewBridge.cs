using System;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public sealed class WeaponReloadViewBridge {
        private WeaponReloadView _reloadView;
        private IReloadPolicy _policy;

        public void Initialize(WeaponReloadView reloadView) {
            _reloadView = reloadView;
        }
        public WeaponReloadViewBridge(IReloadPolicy policy) {
            _policy = policy;
            _policy.ReloadStarted += OnReloadStarted;
            _policy.ReloadStopped += OnReloadStopped;
        }
        public void Dispose() {
            _policy.ReloadStarted -= OnReloadStarted;
            _policy.ReloadStopped -= OnReloadStopped;
        }
        public void OnReloadStarted(float duration) {
            _reloadView.PlayAnimation(duration);
        }
        public void OnReloadStopped() {
            _reloadView.StopAnimation();
        }
    }
}
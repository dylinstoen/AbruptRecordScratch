using UnityEngine;

namespace _Project.Scripts.Actors {
    public sealed class PlayerLookController : MonoBehaviour {
        private IIntentSource _intent;
        private IPlayerCamera _playerCamera;

        public void Initialize(IIntentSource intent) {
            _intent = intent;
        }

        public void BindCamera(IPlayerCamera playerCamera) {
            _playerCamera = playerCamera;
        }

        private void Update() {
            _playerCamera.SetLookInput(_intent.Current.Look);
        }
    }
}
using System;
using _Project.Scripts.Input;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public sealed class PlayerLookController : MonoBehaviour {
        private IPlayerCamera _playerCamera;
        private IIntentSource  _intent;
        private bool _boundCamera = false;

        public void BindIntent(IIntentSource intent) {
            _intent = intent;
        }

        public void BindCamera(IPlayerCamera playerCamera) {
            if(_boundCamera) return;
            _playerCamera = playerCamera;
            _boundCamera = true;
        }

        private void Update() {
            if(!_boundCamera) return;
            _playerCamera.SetLookInput(_intent.Current.Look);
        }
        
    }
}
using System;
using _Project.Input;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public sealed class PlayerPause : MonoBehaviour{
        private PlayerIntentSource _intent;

        private void Start() {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void Update() {
            if (_intent.PauseIntent()) Cursor.visible = !Cursor.visible;
        }

        public void Initialize(PlayerIntentSource intent) {
            _intent = intent;
        }
    }
}
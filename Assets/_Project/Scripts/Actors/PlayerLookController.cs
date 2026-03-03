using System;
using _Project.Scripts.Input;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public sealed class PlayerLookController : MonoBehaviour {
        private ILookCameraSource _lookCameraSource;
        private IIntentSource  _intent;
        private bool _initialized = false;

        public void Initialize(IIntentSource intent, ILookCameraSource lookCameraSource) {
            if(_initialized) return;
            _intent = intent;
            _lookCameraSource = lookCameraSource;
            _initialized = true;
        }

        private void Update() {
            if(!_initialized) return;
            _lookCameraSource.SetLookInput(_intent.Current.Look);
        }
        
    }
}
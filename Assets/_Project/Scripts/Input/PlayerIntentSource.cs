using System;
using _Project.Scripts.Actors;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Input {
    public sealed class PlayerIntentSource : MonoBehaviour, IIntentSource {
        [SerializeField] private PlayerInput playerInput;
        private InputAction _lookAction;
        private InputAction _moveAction;
        private InputAction _fireAction;
        private InputAction _switchDelta;
        private InputAction _pauseAction;
        private bool applicationFocused;
        private bool applicationPausedWithKeyboard;

        public ActorIntent Current { get; private set; }

        
        private void Awake() {
            var actions = playerInput.actions;
            _lookAction = actions.FindAction("Look");
            _moveAction = actions.FindAction("Move");
            _fireAction = actions.FindAction("Fire");
            _switchDelta = actions.FindAction("Switch Delta");
            _pauseAction = actions.FindAction("Pause");
        }
        private void Update() {
            if (playerInput.currentActionMap is not { name: "Gameplay" }) return;
            bool fireHeld = false;
            bool firePressed = false;

            
            if (applicationFocused) {
                if (_fireAction.WasReleasedThisFrame()) {
                    applicationFocused = false;
                }
            }
            else {
                fireHeld = _fireAction.IsPressed();
                firePressed = _fireAction.WasPressedThisFrame();
            }
            
            ActorIntent intent = new ActorIntent {
                Move = _moveAction.ReadValue<Vector2>(),
                Look = _lookAction.ReadValue<Vector2>(),
                FireHeld = fireHeld,
                FirePressed = firePressed,
                SwitchDelta = _switchDelta.ReadValue<float>() 
            };
            Current = intent;
        }
        
        
        private void OnApplicationFocus(bool hasFocus) {
            if (hasFocus) {
                applicationFocused = true;
            }
        }

        public bool PauseIntent() {
            if(playerInput.currentActionMap is not { name: "Gameplay" }) return false;
            return _pauseAction.WasPressedThisFrame();
        }
    }
}
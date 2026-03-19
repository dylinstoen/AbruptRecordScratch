using System;
using _Project.Scripts.Actors;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Input {
    public sealed class PlayerIntentSource : MonoBehaviour, IIntentSource {
        [SerializeField] private PlayerInput playerInput;
        private InputAction _lookAction;
        private InputAction _moveAction;
        private InputAction _primaryFireAction;
        private InputAction _secondaryFireAction;
        private InputAction _interactAction;
        private InputAction _switchDelta;
        private InputAction _pauseAction;
        private bool applicationFocused;
        private bool applicationPausedWithKeyboard;

        public ActorIntent Current { get; private set; }

        
        private void Awake() {
            var actions = playerInput.actions;
            _lookAction = actions.FindAction("Look");
            _moveAction = actions.FindAction("Move");
            _primaryFireAction = actions.FindAction("PrimaryFire");
            _secondaryFireAction = actions.FindAction("SecondaryFire");
            _switchDelta = actions.FindAction("Switch Delta");
            _pauseAction = actions.FindAction("Pause");
            _interactAction = actions.FindAction("Interact");
        }
        private void Update() {
            if (playerInput.currentActionMap is not { name: "Gameplay" }) return;
            bool primaryFireHeld = false;
            bool primaryFirePressed = false;
            bool secondaryFireHeld = false;
            bool secondaryFirePressed = false;
            
            if (applicationFocused) {
                if (_primaryFireAction.WasReleasedThisFrame()) {
                    applicationFocused = false;
                }
            }
            else {
                primaryFireHeld = _primaryFireAction.IsPressed();
                primaryFirePressed = _primaryFireAction.WasPressedThisFrame();
                secondaryFireHeld = _secondaryFireAction.IsPressed();
                secondaryFirePressed = _secondaryFireAction.WasPressedThisFrame();
            }
            
            ActorIntent intent = new ActorIntent {
                Move = _moveAction.ReadValue<Vector2>(),
                Look = _lookAction.ReadValue<Vector2>(),
                PrimaryFireHeld = primaryFireHeld,
                PrimaryFirePressed = primaryFirePressed,
                SecondaryFireHeld = secondaryFireHeld,
                SecondaryFirePressed = secondaryFirePressed,
                SwitchDelta = _switchDelta.ReadValue<float>(),
                Interact = _interactAction.WasReleasedThisFrame()
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
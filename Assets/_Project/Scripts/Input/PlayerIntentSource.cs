using _Project.Scripts.Actors;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Input {
    public sealed class PlayerIntentSource :
        MonoBehaviour,
        IIntentSource {

        [SerializeField] private PlayerInput playerInput;

        private InputAction _lookAction;
        private InputAction _moveAction;
        private InputAction _primaryFireAction;
        private InputAction _secondaryFireAction;
        private InputAction _interactAction;
        private InputAction _switchDelta;

        private bool _applicationFocused;

        public ActorIntent Current { get; private set; }

        private void Awake() {
            var actions = playerInput.actions;

            _lookAction =
                actions.FindAction("Look", true);

            _moveAction =
                actions.FindAction("Move", true);

            _primaryFireAction =
                actions.FindAction("PrimaryFire", true);

            _secondaryFireAction =
                actions.FindAction("SecondaryFire", true);

            _switchDelta =
                actions.FindAction("Switch Delta", true);

            _interactAction =
                actions.FindAction("Interact", true);
        }

        private void Update() {
            if (playerInput.currentActionMap is not { name: "Gameplay" }) {
                Current = default;
                return;
            }

            bool primaryFireHeld = false;
            bool primaryFirePressed = false;

            bool secondaryFireHeld = false;
            bool secondaryFirePressed = false;

            if (_applicationFocused) {
                if (_primaryFireAction.WasReleasedThisFrame()) {
                    _applicationFocused = false;
                }
            }
            else {
                primaryFireHeld =
                    _primaryFireAction.IsPressed();

                primaryFirePressed =
                    _primaryFireAction.WasPressedThisFrame();

                secondaryFireHeld =
                    _secondaryFireAction.IsPressed();

                secondaryFirePressed =
                    _secondaryFireAction.WasPressedThisFrame();
            }

            Current = new ActorIntent {
                Move =
                    _moveAction.ReadValue<Vector2>(),

                Look =
                    _lookAction.ReadValue<Vector2>(),

                PrimaryFireHeld =
                    primaryFireHeld,

                PrimaryFirePressed =
                    primaryFirePressed,

                SecondaryFireHeld =
                    secondaryFireHeld,

                SecondaryFirePressed =
                    secondaryFirePressed,

                SwitchDelta =
                    _switchDelta.ReadValue<float>(),

                Interact =
                    _interactAction.WasReleasedThisFrame()
            };
        }

        private void OnApplicationFocus(bool hasFocus) {
            if (hasFocus) {
                _applicationFocused = true;
            }
        }
    }
}
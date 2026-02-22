using _Project.Scripts.Actors;
using _Project.Scripts.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Input {
    public sealed class PlayerIntentSource : MonoBehaviour, IIntentSource, IPausable {
        private InputAction _lookAction;
        private InputAction _moveAction;
        private InputAction _fireAction;
        private InputAction _switchDelta;
        private InputAction _pauseAction;
        
        public ActorIntent Current { get; private set; }
        
        private void Awake() {
            _lookAction = InputSystem.actions.FindAction("Look");
            _moveAction = InputSystem.actions.FindAction("Move");
            _fireAction = InputSystem.actions.FindAction("Fire");
            _switchDelta = InputSystem.actions.FindAction("Switch Delta");
            _pauseAction = InputSystem.actions.FindAction("Pause");
        }
        private void Update() {
            ActorIntent intent = new ActorIntent {
                Move = _moveAction.ReadValue<Vector2>(),
                Look = _lookAction.ReadValue<Vector2>(),
                FireHeld = _fireAction.IsPressed(),
                FirePressed = _fireAction.WasPressedThisFrame(),
                // TODO: Change SwitchDelta to int and snap to 1, 0 or -1
                SwitchDelta = _switchDelta.ReadValue<float>() 
            };
            Current = intent;
        }
        public bool PauseIntent() => _pauseAction.ReadValue<float>() > 0.5f;
    }
}
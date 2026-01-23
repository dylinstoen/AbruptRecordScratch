using FPS.Input;
using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.Player {
    public class InputReader : MonoBehaviour, IInputReader {
        private InputAction moveAction;
        private InputAction lookAction;
        private InputAction pauseAction;

        private void Awake() {
            moveAction = InputSystem.actions.FindAction("Move");
            lookAction = InputSystem.actions.FindAction("Look");
            pauseAction = InputSystem.actions.FindAction("Pause");
        }

        private void Update() {
            Move();
            Look();
        }

        public Vector2 Move() => moveAction.ReadValue<Vector2>();
        public Vector2 Look() => lookAction.ReadValue<Vector2>();
        public bool Pause() => pauseAction.ReadValue<float>() > 0.5f;
    }
}

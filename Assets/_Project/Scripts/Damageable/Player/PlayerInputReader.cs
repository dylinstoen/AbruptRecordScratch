using UnityEngine;
using UnityEngine.InputSystem;

namespace FPS.Input {
    public class PlayerInputReader : MonoBehaviour, IInputReader {
        private InputAction moveAction;
        private InputAction attackAction;
        private InputAction lookAction;
        private InputAction pauseAction;

        private void Awake() {
            moveAction = InputSystem.actions.FindAction("Move");
            attackAction = InputSystem.actions.FindAction("Attack");
            lookAction = InputSystem.actions.FindAction("Look");
            pauseAction = InputSystem.actions.FindAction("Pause");
        }

        private void Update() {
            Move();
            Attack();
            Look();
        }

        public Vector2 Move() => moveAction.ReadValue<Vector2>();
        public bool Attack() => attackAction.ReadValue<float>() > 0.5f;
        public Vector2 Look() => lookAction.ReadValue<Vector2>();
        public bool Pause() => pauseAction.ReadValue<float>() > 0.5f;
    }
}

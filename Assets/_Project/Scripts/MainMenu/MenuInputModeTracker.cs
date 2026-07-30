using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.MainMenu {
    public enum MenuInputMode {
        Mouse,
        Controller
    }

    public class MenuInputModeTracker : MonoBehaviour {
        public MenuInputMode CurrentMode { get; private set; } = MenuInputMode.Mouse;
        public event Action<MenuInputMode> ModeChanged;
        private Vector2 _lastMousePosition;

        private void Awake() {
            if (Mouse.current != null)
                _lastMousePosition = Mouse.current.position.ReadValue();
        }
        private void Update() {
            DetectMouseInput();
            DetectControllerInput();
        }
        private void DetectMouseInput() {
            if (Mouse.current == null)
                return;

            Vector2 currentPosition = Mouse.current.position.ReadValue();

            bool mouseMoved =
                Vector2.SqrMagnitude(currentPosition - _lastMousePosition) > 0.01f;

            bool mouseClicked =
                Mouse.current.leftButton.wasPressedThisFrame ||
                Mouse.current.rightButton.wasPressedThisFrame;

            _lastMousePosition = currentPosition;

            if (mouseMoved || mouseClicked)
                SetMode(MenuInputMode.Mouse);
        }
        private void DetectControllerInput() {
            if (Gamepad.current == null)
                return;

            bool controllerUsed =
                Gamepad.current.dpad.ReadValue().sqrMagnitude > 0.01f ||
                Gamepad.current.leftStick.ReadValue().sqrMagnitude > 0.25f ||
                Gamepad.current.buttonSouth.wasPressedThisFrame ||
                Gamepad.current.buttonEast.wasPressedThisFrame;

            if (controllerUsed)
                SetMode(MenuInputMode.Controller);
        }
        private void SetMode(MenuInputMode mode) {
            if (CurrentMode == mode)
                return;

            CurrentMode = mode;
            ModeChanged?.Invoke(mode);
        }
    }

}


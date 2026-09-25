using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Input {
    public sealed class InputModeService : MonoBehaviour, IInputModeService {
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private DeathUiInput deathUiInput;
        public IDeathUIIInputEvent DeathUIIInputEvent => deathUiInput; 
        public void SetGameplay()
        {
            playerInput.SwitchCurrentActionMap("Gameplay");
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        public void SetDead()
        {
            playerInput.SwitchCurrentActionMap("Dead");
        }

        public void SetUI() {
            playerInput.SwitchCurrentActionMap("UI");

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
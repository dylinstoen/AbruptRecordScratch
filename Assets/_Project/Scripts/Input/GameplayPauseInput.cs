using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Input {
    public class GameplayPauseInput : MonoBehaviour {
        [SerializeField] private PlayerInput playerInput;
        private InputAction _pauseAction;

        public event Action PauseRequested;

        private void Awake() {
            _pauseAction = playerInput.actions.FindAction("Pause", throwIfNotFound: true);
        }

        private void OnEnable() {
            _pauseAction.performed += OnPausePerformed;
        }

        private void OnDisable() {
            _pauseAction.performed -= OnPausePerformed;
        }
        private void OnPausePerformed(InputAction.CallbackContext context) {
            if (playerInput.currentActionMap is not { name: "Gameplay" })
                return;
            PauseRequested?.Invoke();
        }
    }
}


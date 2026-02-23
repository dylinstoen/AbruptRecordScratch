using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.Input {
    public class DeathUiInput : MonoBehaviour, IDeathUIIInputEvent {
        [SerializeField] private PlayerInput playerInput;
        private InputAction _continueAction;
        public event Action ContinueRequested;

        private void Awake() {
            _continueAction = playerInput.actions.FindAction("Continue");
        }
        private void OnEnable()
        {
            _continueAction.performed += OnContinuePerformed;
        }

        private void OnDisable()
        {
            _continueAction.performed -= OnContinuePerformed;
        }
        private void OnContinuePerformed(InputAction.CallbackContext _)
        {
            if (playerInput.currentActionMap is { name: "Dead" })
            {
                Debug.Log("Continue Requested");
                ContinueRequested?.Invoke();
            }
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.UI.Navigation {
    public sealed class MenuNavigationInput : MonoBehaviour {
        [SerializeField] private PlayerInput playerInput;
        private InputAction _cancelAction;

        [SerializeField]
        private MenuNavigationController navigation;

        private void Awake() {
            _cancelAction = playerInput.actions.FindAction("Cancel", throwIfNotFound: true);
        }
        private void OnEnable() {
            _cancelAction.performed += OnCancel;
        }

        private void OnDisable() {
            _cancelAction.performed -= OnCancel;
        }

        public void OnCancel(InputAction.CallbackContext context) {
            if (playerInput.currentActionMap is not { name: "UI" })
                return;
            if (!context.performed)
                return;
            navigation.RequestBack();
        }
    }
}
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.MainMenu {
    public sealed class MenuNavigationInput : MonoBehaviour {
        [SerializeField] private MenuNavigationController navigation;

        public void OnCancel(InputAction.CallbackContext context) {
            if (!context.performed)
                return;

            bool wentBack = navigation.GoBack();

            if (!wentBack) {
                // Already at root.
                // You could open a quit confirmation or do nothing.
            }
        }
    }

}


using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.MainMenu {
    public sealed class MenuNavigationInput : MonoBehaviour {
        [SerializeField] private MenuNavigationController navigation;

        public void OnCancel(InputAction.CallbackContext context) {
            if (!context.performed)
                return;

            navigation.RequestBack();
        }
    }

}


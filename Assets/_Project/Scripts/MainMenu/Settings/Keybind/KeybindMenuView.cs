using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.MainMenu {
    public sealed class KeybindMenuView :
        MonoBehaviour,
        IKeybindPage {

        [SerializeField]
        private MenuPage _menuPage;

        private KeybindSession _session;

        public MenuPage ThisMenuPage => _menuPage;

        private MenuNavigationController _navigation;

        public void Initialize(MenuNavigationController navigation) {
            _navigation = navigation;
        }

        public void BindSession(KeybindSession session) {
            if (session == null) {
                return;
            }
            _session = session;

            RefreshInteractable();
            RefreshButtons();
        }

        public void Rebind(InputAction action, int bindingIndex) {

            if (_session == null)
                return;

            _session.BeginRebind(
                action,
                bindingIndex,
                onComplete: () => {
                    RefreshInteractable();
                    RefreshButtons();
                },
                onCancel: () => {
                    RefreshInteractable();
                    RefreshButtons();
                }
            );
        }

        public void Apply() {
            if (_session == null)
                return;

            _session.Apply();

            RefreshInteractable();
            RefreshButtons();
        }

        public void RestoreDefaults() {
            if (_session == null)
                return;

            _session.RestoreDefaults();

            RefreshInteractable();
            RefreshButtons();
        }

        public void Back() {
            _navigation.RequestBack();
        }

        private void RefreshInteractable() {
            
        }

        private void RefreshButtons() {

        }
    }
}
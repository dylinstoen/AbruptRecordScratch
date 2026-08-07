using UnityEngine;

namespace _Project.Scripts.MainMenu {
    public sealed class KeybindMenuView :
        MonoBehaviour,
        IKeybindPage {

        [SerializeField]
        private MenuPage _menuPage;

        private KeybindSession _session;

        public MenuPage ThisMenuPage => _menuPage;

        public void BindSession(KeybindSession session) {
            _session = session;

            RefreshBindings();
            RefreshButtons();
        }

        public void Apply() {
            if (_session == null)
                return;

            _session.Apply();

            RefreshBindings();
            RefreshButtons();
        }

        public void RestoreDefaults() {
            if (_session == null)
                return;

            _session.RestoreDefaults();

            RefreshBindings();
            RefreshButtons();
        }

        private void RefreshBindings() {
            // Update the displayed binding strings.
        }

        private void RefreshButtons() {
            // _applyButton.interactable =
            //     _session != null && _session.HasChanges;
        }
    }
}
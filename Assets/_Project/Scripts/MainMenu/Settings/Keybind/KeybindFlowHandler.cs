using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts.MainMenu {
    public sealed class KeybindFlowHandler : MonoBehaviour {
        [SerializeField, Anywhere]
        private InterfaceRef<IKeybindPage> _page;

        private InputBindingController _inputBindingController;
        private MenuNavigationController _navigation;

        public KeybindSession Session { get; private set; }

        public bool IsOpen => Session != null;

        public void Initialize(
            InputBindingController inputBindingController,
            MenuNavigationController navigation
        ) {
            _inputBindingController = inputBindingController;
            _navigation = navigation;

            _navigation.PagePopped += HandlePagePopped;
        }

        public void Open(MenuOption openingButton) {
            if (IsOpen) {
                Debug.LogWarning(
                    "Keybind flow is already open.",
                    this
                );

                return;
            }

            if (_inputBindingController == null ||
                _navigation == null) {

                Debug.LogError(
                    $"{nameof(KeybindFlowHandler)} was not initialized.",
                    this
                );

                return;
            }

            IKeybindPage page = _page.Value;

            if (page == null) {
                Debug.LogError(
                    $"{nameof(KeybindFlowHandler)} contains a missing keybind page.",
                    this
                );

                return;
            }

            Session = new KeybindSession(
                _inputBindingController
            );

            page.BindSession(Session);

            _navigation.OpenSubmenu(
                page.ThisMenuPage,
                openingButton
            );
        }

        private void HandlePagePopped(MenuPage poppedPage) {
            IKeybindPage page = _page.Value;

            if (page == null)
                return;

            if (poppedPage != page.ThisMenuPage)
                return;

            CloseSession();
        }

        private void CloseSession() {
            if (Session == null)
                return;

            // Restore anything changed since the most recent Apply.
            Session.Discard();

            _page.Value?.BindSession(null);

            Session = null;
        }

        private void OnDestroy() {
            if (_navigation != null) {
                _navigation.PagePopped -= HandlePagePopped;
            }
        }
    }
}
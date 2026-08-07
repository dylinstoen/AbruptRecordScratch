using KBCore.Refs;
using UnityEngine;

namespace _Project.Scripts.MainMenu {
    public sealed class SettingsFlowHandler : MonoBehaviour {
        [SerializeField, Anywhere]
        private InterfaceRef<ISettingsPage> _rootPage;

        [SerializeField, Anywhere]
        private InterfaceRef<ISettingsPage>[] _subPages;

        private SettingsController _settingsController;
        private MenuNavigationController _navigation;

        public SettingsSession Session { get; private set; }

        public bool IsOpen => Session != null;

        public void Initialize(
            SettingsController settingsController,
            MenuNavigationController menuNavigationController) {

            _settingsController = settingsController;
            _navigation = menuNavigationController;

            _navigation.PagePopped += HandlePagePopped;
        }

        public void Open(MenuOption openingButton) {
            if (IsOpen) {
                Debug.LogWarning("Settings flow is already open.", this);
                return;
            }

            if (_settingsController == null || _navigation == null) {
                Debug.LogError(
                    $"{nameof(SettingsFlowHandler)} was not initialized.",
                    this
                );

                return;
            }

            Session = new SettingsSession(_settingsController);

            BindPages(Session);
            
            _navigation.OpenSubmenu(
                _rootPage.Value.ThisMenuPage,
                openingButton
            );
        }

        private void BindPages(SettingsSession session) {
            BindPage(_rootPage.Value, session);

            foreach (InterfaceRef<ISettingsPage> pageRef in _subPages) {
                BindPage(pageRef.Value, session);
            }
        }

        private void BindPage(
            ISettingsPage page,
            SettingsSession session) {

            if (page == null) {
                Debug.LogError(
                    $"{nameof(SettingsFlowHandler)} contains a missing settings page.",
                    this
                );

                return;
            }

            page.BindSession(session);
        }

        private void HandlePagePopped(MenuPage poppedPage) {
            ISettingsPage rootPage = _rootPage.Value;
            Debug.Log(poppedPage.name);
            if (rootPage == null)
                return;

            if (poppedPage != rootPage.ThisMenuPage)
                return;

            CloseSession();
        }

        private void CloseSession() {
            BindPages(null);
            Session = null;
        }

        private void OnDestroy() {
            if (_navigation != null)
                _navigation.PagePopped -= HandlePagePopped;
        }
    }
}
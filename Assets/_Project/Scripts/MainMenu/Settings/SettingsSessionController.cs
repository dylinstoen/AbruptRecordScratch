using KBCore.Refs;
using UnityEngine;
using _Project.Scripts.GameRoot;
using _Project.Scripts.UI.Navigation;
namespace _Project.Scripts.MainMenu {
    public sealed class SettingsSessionController : MonoBehaviour {
        [SerializeField, Anywhere]
        private InterfaceRef<ISettingsPage> _rootPage;

        [SerializeField, Anywhere]
        private InterfaceRef<ISettingsPage>[] _subPages;

        private ISettingsService _settingsService;
        private MenuNavigationController _navigation;

        public SettingsSession Session { get; private set; }

        public bool IsOpen => Session != null;

        public void Initialize(
            ISettingsService settingsService,
            MenuNavigationController menuNavigationController) {

            _settingsService = settingsService;
            _navigation = menuNavigationController;

            _navigation.PagePopped += HandlePagePopped;
        }

        public void Open(MenuOption openingButton) {
            if (IsOpen) {
                Debug.LogWarning("Settings flow is already open.", this);
                return;
            }

            if (_settingsService == null || _navigation == null) {
                Debug.LogError($"{nameof(SettingsSessionController)} was not initialized.", this);
                return;
            }

            Session = new SettingsSession(_settingsService);
            
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
                    $"{nameof(SettingsSessionController)} contains a missing settings page.",
                    this
                );

                return;
            }

            page.BindSession(session);
        }

        private void HandlePagePopped(MenuPage poppedPage) {
            ISettingsPage rootPage = _rootPage.Value;
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
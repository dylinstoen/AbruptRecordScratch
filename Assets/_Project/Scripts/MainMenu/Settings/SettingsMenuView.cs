using KBCore.Refs;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.MainMenu {
    public sealed class SettingsMenuView :
        MonoBehaviour, ISettingsPage {

        [SerializeField, Anywhere] private InterfaceRef<ISettingsControl>[] _controls;
        [SerializeField] private MenuOption _keybindButton;
        [SerializeField] private MenuPage _keybindMenuPage;
        [SerializeField] private MenuPage _menuPage;
        private MenuNavigationController _navigation;
        private SettingsController _controller;
        private SettingsSession _session;

        public MenuPage ThisMenuPage => _menuPage;

        public void Initialize(SettingsController controller, MenuPage settingsMenuPage, MenuNavigationController menuNavigationController) {
            _controller = controller;
            _menuPage = settingsMenuPage;
            _navigation = menuNavigationController;

            _menuPage.Shown += OnShown;
            _menuPage.Hidden += OnHidden;
        }

        public void BindSession(SettingsSession settingsSession) {
            _session = settingsSession;

            if (_session != null)
                InitializeControls();
        }

        public void OnKeybindPressed() {
            _navigation.OpenSubmenu(_keybindMenuPage, _keybindButton);
        }

        private void OnShown() {
            if (_session == null) {
                Debug.LogError(
                    $"{nameof(SettingsMenuView)} has no active settings session.",
                    this
                );

                return;
            }

            RefreshControls();
        }

        private void OnHidden() {
            // Do nothing because i want the session to persist even if the screen is hidden due to opening submenus. Closing submenus (I.E going back will discard and set the session to null)
        }

        public void Apply() {
            if (_session == null)
                return;

            _session.Apply();
        }

        public void RestoreDefaults() {
            _session.RestoreDefaults();
            RefreshControls();
        }

        public void Back() {
            _navigation.RequestBack();
        }

        private void InitializeControls() {
            foreach (InterfaceRef<ISettingsControl> controlRef in _controls) {
                ISettingsControl control = controlRef.Value;

                if (control == null) {
                    Debug.LogError(
                        $"{nameof(SettingsMenuView)} contains a missing settings control.",
                        this
                    );
                    continue;
                }

                control.Initialize(_session);
            }
        }
        private void RefreshControls() {
            foreach (InterfaceRef<ISettingsControl> controlRef in _controls) {
                ISettingsControl control = controlRef.Value;

                if (control == null)
                    continue;

                control.Refresh();
            }
        }

        private void OnDestroy() {
            if (_menuPage == null)
                return;

            _menuPage.Shown -= OnShown;
            _menuPage.Hidden -= OnHidden;
        }
    }
}
using KBCore.Refs;
using System.Collections.Generic;
using UnityEngine;
using _Project.Scripts.UI.Navigation;

namespace _Project.Scripts.MainMenu {
    public sealed class SettingsMenuController :
        MonoBehaviour, ISettingsPage {

        [SerializeField, Anywhere] private InterfaceRef<ISettingsControl>[] _controls;

        [SerializeField] private MenuPage _menuPage;
        [SerializeField] MenuOption openKeyBindButton;
        private MenuNavigationController _navigation;
        private SettingsSession _session;

        private KeybindSessionController _keybindSessionController;

        public MenuPage ThisMenuPage => _menuPage;

        private void Awake() {
            _menuPage.Shown += OnShown;
            _menuPage.Hidden += OnHidden;
        }

        public void Initialize(KeybindSessionController keybindSessionController, MenuNavigationController menuNavigationController) {
            _navigation = menuNavigationController;
            _keybindSessionController = keybindSessionController;
        }

        public void OpenKeybindMenu() {
            _keybindSessionController.Open(openKeyBindButton);
        }

        public void BindSession(SettingsSession settingsSession) {
            _session = settingsSession;
            

            if (_session != null) {
                InitializeControls();
            }
                
        }


        private void OnShown() {
            if (_session == null) {
                Debug.LogError(
                    $"{nameof(SettingsMenuController)} has no active settings session.",
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
                        $"{nameof(SettingsMenuController)} contains a missing settings control.",
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
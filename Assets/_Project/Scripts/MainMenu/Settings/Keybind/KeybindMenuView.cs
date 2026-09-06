using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.MainMenu {
    public sealed class KeybindMenuView :
        MonoBehaviour,
        IKeybindPage {

        [Header("Page")]
        [SerializeField] private MenuPage _menuPage;

        [Header("Keybind UI")]
        [SerializeField] private ActionContainer _actionContainer;
        [SerializeField] private KeybindOptionPromptView _optionPrompt;
        [SerializeField] private RebindPromptView _rebindPrompt;


        [Header("Rebind")]
        [SerializeField] private float _timeoutSeconds = 5f;
        

        private KeybindSession _session;
        private MenuNavigationController _navigation;

        private InputAction _selectedAction;
        private int _selectedBindingIndex = -1;

        public MenuPage ThisMenuPage => _menuPage;

        private float _rebindTimeRemaining;
        private bool _isRebinding;

        private void Update() {
            if (!_isRebinding)
                return;

            _rebindTimeRemaining -= Time.unscaledDeltaTime;
            _rebindPrompt.SetTime(_rebindTimeRemaining);
        }

        public void Initialize(MenuNavigationController navigation) {
            _navigation = navigation;

            _optionPrompt.Initialize(this, navigation);
            _rebindPrompt.Hide();
        }

        public void BindSession(KeybindSession session) {
            if (session == null)
                return;

            _session = session;
            RefreshButtons();
        }

        public void OpenRebindPrompt(InputAction action, int bindingIndex, string bindingLabel, string actionLabel, MenuOption optionThatOpenedIt) {
            if (_session == null)
                return;

            _selectedBindingIndex = bindingIndex;
            _selectedAction = action;
            SetMenuInteractable(false);
            _optionPrompt.Show(actionLabel, bindingLabel, optionThatOpenedIt);
        }

        public void ReplaceBinding() {
            if (_session == null || _selectedAction == null)
                return;
            _optionPrompt.Hide();

            _rebindTimeRemaining = _timeoutSeconds;
            _isRebinding = true;

            _rebindPrompt.Show(_timeoutSeconds);

            _session.BeginRebind(_selectedAction, _selectedBindingIndex, onComplete: FinishRebind, onCancel: FinishRebind, timeoutSeconds: _timeoutSeconds);
        }

        public void RemoveBinding() {
            if (_session == null || _selectedAction == null) {
                return;
            }
            _session.RemoveBinding(_selectedAction, _selectedBindingIndex);
            CloseBindingOptions();
            RefreshButtons();
        }

        public void CloseBindingOptions() {
            _optionPrompt.Hide();
            ClearSelection();
            
            SetMenuInteractable(true);
        }

        public void Apply() {
            if (_session == null)
                return;

            _session.Apply();
            RefreshButtons();
        }

        public void RestoreDefaults() {
            if (_session == null)
                return;

            _session.RestoreDefaults();
            RefreshButtons();
        }

        public void Back() {
            if (_session == null)
                return;

            _session.CloseAndDiscard();
            _navigation.RequestBack();
        }

        private void FinishRebind() {
            _isRebinding = false;
            _rebindPrompt.Hide();

            ClearSelection();
            SetMenuInteractable(true);

            RefreshButtons();
        }

        private void ClearSelection() {
            _selectedAction = null;
            _selectedBindingIndex = -1;
        }

        private void SetMenuInteractable(bool interactable) {
            _menuPage.SetInteractable(interactable);
        }

        private void RefreshButtons() {
            _actionContainer.Refresh();
        }
    }
}
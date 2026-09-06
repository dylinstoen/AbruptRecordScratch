using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.MainMenu {
    public class MenuPage : MonoBehaviour {
        [Header("Navigation")]
        [SerializeField] private MenuOption defaultOption;
        [SerializeField] private MenuPagePresentation presentation =MenuPagePresentation.Replace;

        private MenuOption[] _options;

        public event Action Shown;
        public event Action Hidden;

        private MenuInputModeTracker _inputModeTracker;
        private MenuOption _lastSelectedOption;
        private IMenuBackHandler _backHandler;

        public MenuOption GetLastSelectedOption => _lastSelectedOption;
        public MenuPagePresentation Presentation => presentation;

        public void Initialize(MenuInputModeTracker inputModeTracker) {
            _inputModeTracker = inputModeTracker;
            _backHandler = GetComponent<IMenuBackHandler>();

            CollectOwnedOptions();

            foreach (MenuOption option in _options) {
                option.Initialize(this, inputModeTracker);
            }

            InitializeChildPages();
        }

        public void Show(MenuOption preferredOption = null) {
            gameObject.SetActive(true);

            Shown?.Invoke();

            if (_inputModeTracker.CurrentMode == MenuInputMode.Controller) {
                MenuOption optionToSelect =
                    preferredOption ??
                    _lastSelectedOption ??
                    defaultOption;

                Select(optionToSelect);
            }
            else {
                ClearSelection();
            }
        }

        public void Hide() {
            ResetVisuals();
            ClearSelection();

            Hidden?.Invoke();

            gameObject.SetActive(false);
        }

        public void SetInteractable(bool interactable) {
            foreach (MenuOption option in _options) {
                option.SetInteractable(interactable);
            }
        }

        public bool TryHandleBack() {
            return _backHandler != null &&
                   _backHandler.TryHandleBack();
        }

        public void RememberSelection(MenuOption option) {
            _lastSelectedOption = option;
        }

        public void RestoreSelection() {
            if (_inputModeTracker.CurrentMode != MenuInputMode.Controller)
                return;

            Select(_lastSelectedOption ?? defaultOption);
        }

        public void ClearCurrentSelection() {
            ClearSelection();
        }

        private void CollectOwnedOptions() {
            MenuOption[] allOptions =
                GetComponentsInChildren<MenuOption>(true);

            List<MenuOption> ownedOptions = new();

            foreach (MenuOption option in allOptions) {
                // Finds the closest MenuPage above this option.
                MenuPage owningPage =
                    option.GetComponentInParent<MenuPage>(true);

                // Only collect it if THIS is its closest page.
                if (owningPage != this)
                    continue;

                ownedOptions.Add(option);
            }

            _options = ownedOptions.ToArray();
        }

        private void InitializeChildPages() {
            MenuPage[] allPages =
                GetComponentsInChildren<MenuPage>(true);

            foreach (MenuPage page in allPages) {
                if (page == this)
                    continue;

                // Find the closest MenuPage above this page,
                // excluding the page itself.
                MenuPage parentPage =
                    page.transform.parent != null
                        ? page.transform.parent.GetComponentInParent<MenuPage>(true)
                        : null;

                // Only initialize direct child pages.
                if (parentPage != this)
                    continue;

                page.Initialize(_inputModeTracker);
            }
        }

        private void ResetVisuals() {
            foreach (MenuOption option in _options) {
                option.ResetVisualState();
            }
        }

        private static void Select(MenuOption option) {
            if (option == null)
                return;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(
                option.GameObject
            );
        }

        private static void ClearSelection() {
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
}
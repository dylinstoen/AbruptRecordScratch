using System;
using UnityEngine;
using UnityEngine.EventSystems;
namespace _Project.Scripts.MainMenu {
    public class MenuPage : MonoBehaviour {
        [Header("Navigation")]
        [SerializeField] private MenuOption defaultOption;
        [SerializeField] private MenuOption[] options;
        public event Action Shown;
        public event Action Hidden;

        private MenuInputModeTracker _inputModeTracker;
        private MenuOption _lastSelectedOption;
        private IMenuBackHandler _backHandler;

        public MenuOption GetLastSelectedOption { get { return _lastSelectedOption; }  }

        public void Initialize(MenuInputModeTracker inputModeTracker) {
            _inputModeTracker = inputModeTracker;
            _backHandler = GetComponent<IMenuBackHandler>();

            foreach (MenuOption option in options) {
                option.Initialize(this, inputModeTracker);
            }
                
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
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
        public bool TryHandleBack() {
            return _backHandler != null && _backHandler.TryHandleBack();
        }

        public void Hide() {
            foreach (MenuOption option in options)
                option.ResetVisualState();

            Hidden?.Invoke();
            gameObject.SetActive(false);
        }

        public void RememberSelection(MenuOption option) {
            _lastSelectedOption = option;
        }

        public void RestoreSelection() {
            if (_inputModeTracker.CurrentMode != MenuInputMode.Controller)
                return;

            Select(_lastSelectedOption ?? defaultOption);
        }

        private static void Select(MenuOption option) {
            if (option == null)
                return;

            EventSystem.current.SetSelectedGameObject(null);
            EventSystem.current.SetSelectedGameObject(option.GameObject);
        }
    }
}

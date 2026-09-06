using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.MainMenu {
    public class MenuNavigationController : MonoBehaviour {
        [SerializeField] private MenuPage defaultPage;
        [SerializeField] private MenuInputModeTracker inputModeTracker;

        public void PrintHistory() {
            Debug.Log($"History size: {_history.Count}");
        }

        private readonly Stack<MenuHistoryEntry> _history = new();

        private MenuPage _currentPage;

        public event System.Action<MenuPage> PagePopped;

        private readonly struct MenuHistoryEntry {
            public readonly MenuPage Page;
            public readonly MenuOption ReturnOption;
            public readonly bool WasHidden;

            public MenuHistoryEntry(
                MenuPage page,
                MenuOption returnOption,
                bool wasHidden
            ) {
                Page = page;
                ReturnOption = returnOption;
                WasHidden = wasHidden;
            }
        }

        private void Awake() {
            inputModeTracker.ModeChanged += OnInputModeChanged;
        }

        private void Start() {
            OpenRoot(defaultPage);
        }

        private void OnDestroy() {
            inputModeTracker.ModeChanged -= OnInputModeChanged;
        }

        private void OnInputModeChanged(MenuInputMode mode) {
            if (_currentPage == null)
                return;

            if (mode == MenuInputMode.Mouse) {
                UnityEngine.EventSystems.EventSystem.current
                    .SetSelectedGameObject(null);
            }
            else {
                _currentPage.RestoreSelection();
            }
        }

        public void OpenRoot(MenuPage rootPage) {
            _history.Clear();

            if (_currentPage != null)
                _currentPage.Hide();

            _currentPage = rootPage;
            _currentPage.Show();
        }

        public void OpenSubmenu(
            MenuPage submenu,
            MenuOption optionThatOpenedIt
        ) {
            if (_currentPage == null)
                return;

            bool hideCurrent =
                submenu.Presentation ==
                MenuPagePresentation.Replace;

            _history.Push(
                new MenuHistoryEntry(
                    _currentPage,
                    optionThatOpenedIt,
                    hideCurrent
                )
            );

            if (hideCurrent) {
                _currentPage.Hide();
            }
            else {
                // Overlay page:
                // keep the parent visible, but disable its options.
                _currentPage.SetInteractable(false);
                _currentPage.ClearCurrentSelection();
            }

            _currentPage = submenu;
            _currentPage.Show();
        }

        public void RequestBack() {
            if (_currentPage != null &&
                _currentPage.TryHandleBack()) {
                return;
            }

            GoBack();
        }

        private bool GoBack() {
            if (_history.Count == 0)
                return false;

            MenuPage pageBeingPopped = _currentPage;
            MenuHistoryEntry previous = _history.Pop();

            pageBeingPopped.Hide();

            PagePopped?.Invoke(pageBeingPopped);

            _currentPage = previous.Page;

            if (previous.WasHidden) {
                // Normal page transition:
                // previous page actually needs to be shown again.
                _currentPage.Show(previous.ReturnOption);
            }
            else {
                // Overlay transition:
                // previous page never disappeared.
                _currentPage.SetInteractable(true);

                if (previous.ReturnOption != null) {
                    _currentPage.RestoreSelection();
                }
            }

            return true;
        }
    }
}
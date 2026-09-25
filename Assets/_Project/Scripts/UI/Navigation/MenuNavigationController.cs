using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts.UI.Navigation {
    public class MenuNavigationController : MonoBehaviour {
        [SerializeField] private bool openOnStartup = true;
        [SerializeField] private MenuPage defaultPage;
        [SerializeField] private MenuInputModeTracker inputModeTracker;

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
            if (openOnStartup) {
                OpenRoot(defaultPage);
            }
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
            Close();

            _currentPage = rootPage;
            _currentPage.Show();
        }

        public void Close() {
            _history.Clear();

            if (_currentPage != null) {
                _currentPage.Hide();
                _currentPage = null;
            }
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
                _currentPage.SetInteractable(false);
                _currentPage.ClearCurrentSelection();
            }

            _currentPage = submenu;
            _currentPage.Show();
        }

        public void RequestBack() {
            if (_currentPage == null)
                return;

            if (_currentPage.TryHandleBack()) {
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
                _currentPage.Show(previous.ReturnOption);
            }
            else {
                _currentPage.SetInteractable(true);
                _currentPage.RestoreSelection();
            }

            return true;
        }
    }
}
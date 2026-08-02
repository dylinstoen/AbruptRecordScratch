using System.Collections.Generic;
using UnityEngine;
namespace _Project.Scripts.MainMenu {
    public class MenuNavigationController : MonoBehaviour {
        private readonly Stack<MenuHistoryEntry> _history = new();

        private MenuPage _currentPage;
        [SerializeField] private MenuInputModeTracker inputModeTracker;

        private void Awake() {
            inputModeTracker.ModeChanged += OnInputModeChanged;
        }

        private void OnInputModeChanged(MenuInputMode mode) {
            if (_currentPage == null)
                return;

            if (mode == MenuInputMode.Mouse) {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);
            }
            else {
                _currentPage.RestoreSelection();
            }
        }

        private void OnDestroy() {
            inputModeTracker.ModeChanged -= OnInputModeChanged;
        }
        private readonly struct MenuHistoryEntry {
            public readonly MenuPage Page;
            public readonly MenuOption ReturnOption;

            public MenuHistoryEntry(MenuPage page, MenuOption returnOption) {
                Page = page;
                ReturnOption = returnOption;
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
            MenuOption optionThatOpenedIt) {

            if (_currentPage == null)
                return;

            _history.Push(new MenuHistoryEntry(_currentPage, optionThatOpenedIt));

            _currentPage.Hide();

            _currentPage = submenu;
            _currentPage.Show();
        }

        public void RequestBack() {
            if (_currentPage != null && _currentPage.TryHandleBack()) {
                return;
            }
            GoBack();
        }
        public bool GoBack() {
            if (_history.Count == 0)
                return false;

            _currentPage.Hide();

            MenuHistoryEntry previous = _history.Pop();

            _currentPage = previous.Page;
            _currentPage.Show(previous.ReturnOption);

            return true;
        }

    }
}

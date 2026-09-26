using UnityEngine;
using _Project.Scripts.UI.Navigation;
using _Project.Scripts.GameRoot;
namespace _Project.Scripts.MainMenu {
    public class MainMenuController : MonoBehaviour {
        [SerializeField] private MenuNavigationController navigation;

        [Header("Pages")]
        [SerializeField] private MenuPage settingsPage;
        [SerializeField] private MenuPage levelSelectPage;

        [Header("Options")]
        [SerializeField] private MenuOption newGameOption;
        [SerializeField] private MenuOption levelSelectOption;
        [SerializeField] private MenuOption settingsOption;
        [Header("Prompt")]
        [SerializeField] private MenuPage resetProgressPrompt;

        private SettingsSessionController _settingsSessionController;
        private IProgressService _progressService;
        
        public void Initalize(SettingsSessionController settingsSessionController, IProgressService progressService) {
            _settingsSessionController = settingsSessionController;
            _progressService = progressService;
        }

        public void OnNewGame() {
            if (_progressService == null)
                return;
            if (_progressService.HasProgress) {
                navigation.OpenSubmenu(resetProgressPrompt, newGameOption);
                return;
            }

        }

        public void OpenSettings() {
            _settingsSessionController.Open(settingsOption);
        }

        public void OnLevelSelectPressed() {
            navigation.OpenSubmenu(levelSelectPage, levelSelectOption);
        }

        public void OnQuit() {
            Application.Quit();
        }

    }
}

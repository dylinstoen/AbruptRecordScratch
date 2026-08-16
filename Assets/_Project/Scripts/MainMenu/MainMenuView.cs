using UnityEngine;
namespace _Project.Scripts.MainMenu {
    public class MainMenuView : MonoBehaviour {
        [SerializeField] private MenuNavigationController navigation;

        [Header("Pages")]
        [SerializeField] private MenuPage settingsPage;
        [SerializeField] private MenuPage levelSelectPage;

        [Header("Options")]
        [SerializeField] private MenuOption levelSelectOption;
        [SerializeField] private MenuOption settingsOption;

        private SettingsFlowHandler _settingsFlowHandler;
        public void Initalize(SettingsFlowHandler settingsFlowHandler) {
            _settingsFlowHandler = settingsFlowHandler;
        }

        public void OpenSettings() {
            _settingsFlowHandler.Open(settingsOption);
        }

        public void OnLevelSelectPressed() {
            navigation.OpenSubmenu(
                levelSelectPage,
                levelSelectOption);
        }

        public void OnQuit() {
            Application.Quit();
        }

    }
}

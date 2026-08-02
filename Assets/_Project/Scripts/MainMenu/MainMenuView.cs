using UnityEngine;
namespace _Project.Scripts.MainMenu {
    public class MainMenuView : MonoBehaviour {
        [SerializeField] private MenuNavigationController navigation;

        [Header("Pages")]
        [SerializeField] private MenuPage settingsPage;
        [SerializeField] private MenuPage levelSelectPage;

        [Header("Options")]
        [SerializeField] private MenuOption settingsOption;
        [SerializeField] private MenuOption levelSelectOption;

        public void OnSettingsPressed() {
            navigation.OpenSubmenu(
                settingsPage,
                settingsOption);
        }

        public void OnLevelSelectPressed() {
            navigation.OpenSubmenu(
                levelSelectPage,
                levelSelectOption);
        }

    }
}

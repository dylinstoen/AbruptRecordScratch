using UnityEngine;
namespace _Project.Scripts.MainMenu {
    public class MainMenuView : MonoBehaviour {
        [SerializeField] private MenuNavigationController navigation;

        [Header("Pages")]
        [SerializeField] private MenuPage settingsPage;
        [SerializeField] private MenuPage levelSelectPage;

        [Header("Options")]
        [SerializeField] private MenuOption levelSelectOption;

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

using UnityEngine;
namespace _Project.Scripts.MainMenu {
    public class MenuBootstrap : MonoBehaviour {
        [SerializeField] private MenuInputModeTracker inputModeTracker;
        [SerializeField] private MenuNavigationController navigation;

        [SerializeField] private MenuPage mainMenu;
        [SerializeField] private MenuPage settingsMenu;
        [SerializeField] private MenuPage levelSelectMenu;
        //[SerializeField] private MenuPage audioMenu;
        //[SerializeField] private MenuPage videoMenu;

        private void Start() {
            mainMenu.Initialize(inputModeTracker);
            settingsMenu.Initialize(inputModeTracker);
            levelSelectMenu.Initialize(inputModeTracker);
            //audioMenu.Initialize(inputModeTracker);
            //videoMenu.Initialize(inputModeTracker);

            navigation.OpenRoot(mainMenu);
        }
    }
}

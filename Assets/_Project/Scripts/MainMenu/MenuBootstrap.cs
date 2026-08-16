using System;
using UnityEngine;
namespace _Project.Scripts.MainMenu {
    public class MenuBootstrap : MonoBehaviour {
        
        [SerializeField] private MenuInputModeTracker inputModeTracker;
        [SerializeField] private MenuNavigationController navigation;

        [Header("Menu Page")]
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private MenuPage mainMenu;
        [SerializeField] private MenuPage settingsMenu;
        [SerializeField] private MenuPage levelSelectMenu;
        [SerializeField] private MenuPage keyBindMenuPage;

        [Header("Settings")]
        [SerializeField] private SettingsController settingsController;
        [SerializeField] private SettingsMenuView settingsMenuView;
        [SerializeField] private SettingsFlowHandler settingsFlowHandler;
        [SerializeField] private KeybindMenuView keybindMenuView;
        [SerializeField] private InputBindingController inputBindingController;
        [SerializeField] private KeybindFlowHandler keybindFlowHandler;
        //[SerializeField] private MenuPage audioMenu;
        //[SerializeField] private MenuPage videoMenu;

        private void Start() {
            keybindFlowHandler.Initialize(inputBindingController, navigation);
            settingsFlowHandler.Initialize(settingsController, navigation);
            mainMenuView.Initalize(settingsFlowHandler);
            mainMenu.Initialize(inputModeTracker);
            settingsMenu.Initialize(inputModeTracker);
            levelSelectMenu.Initialize(inputModeTracker);
            keyBindMenuPage.Initialize(inputModeTracker);
            settingsMenuView.Initialize(keybindFlowHandler, settingsMenu, navigation);
            keybindMenuView.Initialize(navigation);
            
            
            

            levelSelectMenu.gameObject.SetActive(false);
            settingsMenu.gameObject.SetActive(false);
            keybindMenuView.gameObject.SetActive(false);
            //audioMenu.Initialize(inputModeTracker);
            //videoMenu.Initialize(inputModeTracker);

            navigation.OpenRoot(mainMenu);
        }
    }
}

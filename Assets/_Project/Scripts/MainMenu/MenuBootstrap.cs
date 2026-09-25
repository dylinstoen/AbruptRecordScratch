using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
using _Project.Scripts.GameRoot;
using _Project.Scripts.UI.Navigation;
namespace _Project.Scripts.MainMenu {
    public class MenuBootstrap : MonoBehaviour {

        
        [SerializeField] private MenuInputModeTracker inputModeTracker;
        [SerializeField] private MenuNavigationController navigation;

        [Header("Menu Page")]
        [SerializeField] private MainMenuController mainMenuController;
        [SerializeField] private MenuPage[] menuPages;


        [Header("Settings")]
        [SerializeField] private SettingsService settingsService;
        [SerializeField] private SettingsMenuController settingsMenuController;
        [SerializeField] private SettingsFlowHandler settingsFlowHandler;
        [SerializeField] private KeybindMenuController keybindMenuController;
        [SerializeField] private InputBindingController inputBindingController;
        [SerializeField] private KeybindFlowHandler keybindFlowHandler;
        [SubHeader("Keybind")]
        [SerializeField] private InputActionAsset actions;
        [SerializeField] private ActionContainer actionContainer;

       
        //[SerializeField] private MenuPage audioMenu;
        //[SerializeField] private MenuPage videoMenu;

        private void Start() {
            keybindFlowHandler.Initialize(inputBindingController, navigation);
            settingsFlowHandler.Initialize(settingsService, navigation);
            mainMenuController.Initalize(settingsFlowHandler);
            settingsMenuController.Initialize(keybindFlowHandler, navigation);

            actionContainer.Initialize(actions.FindActionMap("Gameplay"), keybindMenuController);
            keybindMenuController.Initialize(navigation);

            foreach(MenuPage page in menuPages) {
                page.Initialize(inputModeTracker);
                page.gameObject.SetActive(false);
            }
            
            //audioMenu.Initialize(inputModeTracker);
            //videoMenu.Initialize(inputModeTracker);
        }
    }
}

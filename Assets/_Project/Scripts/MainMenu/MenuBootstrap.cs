using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.InputSystem;
namespace _Project.Scripts.MainMenu {
    public class MenuBootstrap : MonoBehaviour {

        
        [SerializeField] private MenuInputModeTracker inputModeTracker;
        [SerializeField] private MenuNavigationController navigation;

        [Header("Menu Page")]
        [SerializeField] private MainMenuView mainMenuView;
        [SerializeField] private MenuPage[] menuPages;


        [Header("Settings")]
        [SerializeField] private SettingsController settingsController;
        [SerializeField] private SettingsMenuView settingsMenuView;
        [SerializeField] private SettingsFlowHandler settingsFlowHandler;
        [SerializeField] private KeybindMenuView keybindMenuView;
        [SerializeField] private InputBindingController inputBindingController;
        [SerializeField] private KeybindFlowHandler keybindFlowHandler;
        [SubHeader("Keybind")]
        [SerializeField] private InputActionAsset actions;
        [SerializeField] private ActionContainer actionContainer;

       
        //[SerializeField] private MenuPage audioMenu;
        //[SerializeField] private MenuPage videoMenu;

        private void Start() {
            keybindFlowHandler.Initialize(inputBindingController, navigation);
            settingsFlowHandler.Initialize(settingsController, navigation);
            mainMenuView.Initalize(settingsFlowHandler);
            settingsMenuView.Initialize(keybindFlowHandler, navigation);

            actionContainer.Initialize(actions.FindActionMap("Gameplay"), keybindMenuView);
            keybindMenuView.Initialize(navigation);

            foreach(MenuPage page in menuPages) {
                page.Initialize(inputModeTracker);
                page.gameObject.SetActive(false);
            }
            
            //audioMenu.Initialize(inputModeTracker);
            //videoMenu.Initialize(inputModeTracker);
        }
    }
}

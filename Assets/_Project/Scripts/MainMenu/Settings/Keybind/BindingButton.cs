using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using _Project.Scripts.UI.Navigation;
namespace _Project.Scripts.MainMenu {
    public class BindingButton : MonoBehaviour {
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private MenuOption menuOption;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text bindingLabel;

        private InputAction _action;
        private int _bindingIndex;

        private KeybindMenuController _keybindMenuController;

        string _actionLabel;

        internal void Initialize(InputAction action, int bindingIndex, KeybindMenuController keybindMenuController, string actionLabel) {
            _keybindMenuController = keybindMenuController;
            _action = action;
            _bindingIndex = bindingIndex;
            _actionLabel = actionLabel;

            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;

            button.onClick.AddListener(OpenRebindPrompt);
            Refresh();
        }

        public void InitializeEmpty() {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;

        }

        public void Refresh() {
            if (_action == null)
                return;

            InputBinding binding = _action.bindings[_bindingIndex];

            if (string.IsNullOrEmpty(binding.effectivePath)) {
                bindingLabel.text = "-";
                return;
            }

            string display = _action.GetBindingDisplayString(
                _bindingIndex,
                InputBinding.DisplayStringOptions.DontIncludeInteractions
            );

            bindingLabel.text = string.IsNullOrEmpty(display) ? "-" : display;
        }

        private void OpenRebindPrompt() {
            _keybindMenuController.OpenRebindPrompt(_action, _bindingIndex, bindingLabel.text, _actionLabel, menuOption);
        }
    }
}


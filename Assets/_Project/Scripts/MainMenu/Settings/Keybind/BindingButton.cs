using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
namespace _Project.Scripts.MainMenu {
    public class BindingButton : MonoBehaviour {
        [SerializeField] private MenuOption menuOption;
        [SerializeField] private Button button;
        [SerializeField] private TMP_Text bindingLabel;

        private InputAction _action;
        private int _bindingIndex;

        private KeybindMenuView _keybindMenuView;

        string _actionLabel;

        internal void Initialize(InputAction action, int bindingIndex, KeybindMenuView keybindMenuView, string actionLabel) {
            _keybindMenuView = keybindMenuView;
            _action = action;
            _bindingIndex = bindingIndex;
            _actionLabel = actionLabel;
            button.onClick.AddListener(OpenRebindPrompt);
            Refresh();
        }

        public void InitializeEmpty() {
            bindingLabel.text = "-";
            button.interactable = false;
        }

        public void Refresh() {
            if(_action ==  null) {
                return;
            }
            string display = _action.GetBindingDisplayString(_bindingIndex);
            bindingLabel.text = string.IsNullOrEmpty(display) ? "-" : display;
        }

        private void OpenRebindPrompt() {
            _keybindMenuView.OpenRebindPrompt(_action, _bindingIndex, bindingLabel.text, _actionLabel, menuOption);
        }
    }
}


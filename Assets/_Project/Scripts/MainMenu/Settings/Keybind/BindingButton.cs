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
        internal void Initialize(InputAction action, int bindingIndex) {
            _action = action;
            _bindingIndex = bindingIndex;
            button.onClick.AddListener(BeginRebind);
            Refresh();
        }

        public void InitializeEmpty() {
            bindingLabel.text = "-";
            button.interactable = false;
        }

        public void Refresh() {
            bindingLabel.text = _action.GetBindingDisplayString(_bindingIndex);
        }

        private void BeginRebind() {
            // TODO: Begin Rebind operation
        }
    }
}


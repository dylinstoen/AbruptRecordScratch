using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.MainMenu {
    public class ActionRow : MonoBehaviour {
        [SerializeField] private TMP_Text actionLabel;
        [Header("Binding Containers")]
        [SerializeField] private Transform keyboardParent;
        [SerializeField] private Transform mouseParent;
        [SerializeField] private Transform gamepadParent;
        [Header("Prefab")]
        [SerializeField] private BindingButton bindingButtonPrefab;

        private InputAction _action;
        private readonly List<int> _keyboardBindings = new();
        private readonly List<int> _mouseBindings = new();
        private readonly List<int> _gamepadBindings = new();

        private readonly List<BindingButton> _spawnedButtons = new();

        public void Initialize(InputAction action, string displayName) {
            _action = action;
            actionLabel.text = displayName;
        }

        public void AddBinding(BindingDevice device, int bindingIndex) {
            switch(device) {
                case BindingDevice.Keyboard:
                    _keyboardBindings.Add(bindingIndex);
                    break;
                case BindingDevice.Mouse:
                    _mouseBindings.Add(bindingIndex);
                    break;
                case BindingDevice.Gamepad:
                    _gamepadBindings.Add(bindingIndex);
                    break;
            }
        }
        public void Refresh() {
            ClearButtons();
            int slotCount = Mathf.Max(_keyboardBindings.Count, _mouseBindings.Count, _gamepadBindings.Count);

            CreateButtons(_keyboardBindings,keyboardParent, slotCount);
            CreateButtons(_mouseBindings, mouseParent, slotCount);
            CreateButtons(_gamepadBindings,gamepadParent, slotCount);
        }

        private void CreateButtons(List<int> bindings, Transform parent, int slotCount) {
            for (int i = 0; i < slotCount; i++) {
                BindingButton button = Instantiate(bindingButtonPrefab, parent);
                _spawnedButtons.Add(button);

                if (i < bindings.Count) {
                    button.Initialize(_action, bindings[i]);
                }
                else {
                    button.InitializeEmpty();
                }
            }

        } 

        private void ClearButtons() {
            foreach (BindingButton button in _spawnedButtons) {
                if (button != null)
                    Destroy(button.gameObject);
            }

            _spawnedButtons.Clear();
        }

        public MenuOption[] GetOptions() {
            return GetComponentsInChildren<MenuOption>(true);
        }
    }
}


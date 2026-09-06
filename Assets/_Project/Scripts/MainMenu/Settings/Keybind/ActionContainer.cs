using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace _Project.Scripts.MainMenu {
    public class ActionContainer : MonoBehaviour {
        [SerializeField] private ActionRow actionRowPrefab;
        [SerializeField] private Transform rowParent;
        private readonly Dictionary<RowKey, ActionRow> _rows = new();

        public void Initialize(InputActionMap actionMap, KeybindMenuView keybindMenuView) {
            Clear();
            foreach(InputAction action in actionMap.actions) {
                BuildRows(action, keybindMenuView);
            }
            foreach(ActionRow row in _rows.Values) {
                row.Build();
            }
        }

        public void Refresh() {
            foreach (ActionRow row in _rows.Values) {
                row.Refresh();
            }
        }

        private void BuildRows(InputAction action, KeybindMenuView keybindMenuView) {
            for(int bindingIndex = 0; bindingIndex < action.bindings.Count; bindingIndex++) {
                InputBinding binding = action.bindings[bindingIndex];
                if (binding.isComposite)
                    continue;
                BindingDevice device = GetDevice(binding);
                RowKey key = GetRowKey(action, bindingIndex);
                if(!_rows.TryGetValue(key, out ActionRow row)) {
                    row = Instantiate(actionRowPrefab, rowParent);
                    row.Initialize(action, GetRowDisplayName(action, action.bindings[bindingIndex]), keybindMenuView);
                    _rows.Add(key, row);
                }
                row.AddBinding(device, bindingIndex);
            }
            
        }

        private void Clear() {
            foreach(Transform child in rowParent) {
                Destroy(child.gameObject);
            }
            _rows.Clear();
        }
        private string GetRowDisplayName(InputAction action, InputBinding binding) {
            string actionName = FormatActionName(action.name);

            if (!binding.isPartOfComposite)
                return actionName;

            if (action.name == "Switch Delta") {
                return binding.name switch {
                    "positive" => "Next Weapon",
                    "negative" => "Previous Weapon",
                    _ => actionName
                };
            }

            return binding.name switch {
                "up" => $"{actionName} Up",
                "down" => $"{actionName} Down",
                "left" => $"{actionName} Left",
                "right" => $"{actionName} Right",
                _ => $"{actionName} {FormatName(binding.name)}"
            };
        }

        private string FormatActionName(string actionName) {
            return actionName switch {
                "PrimaryFire" => "Fire",
                "SecondaryFire" => "Alt Fire",
                _ => actionName
            };
        }

        private string FormatName(string value) => string.IsNullOrEmpty(value) ? "" : char.ToUpper(value[0]) + value.Substring(1);

        private BindingDevice GetDevice(InputBinding binding) {
            if (binding.groups.Contains("Gamepad"))
                return BindingDevice.Gamepad;

            if (binding.groups.Contains("Mouse"))
                return BindingDevice.Mouse;

            if (binding.groups.Contains("Keyboard"))
                return BindingDevice.Keyboard;

            throw new InvalidOperationException(
                $"No device associated with binding '{binding.name}'"
            );
        }

        private RowKey GetRowKey(InputAction action, int bindingIndex) {
            InputBinding binding = action.bindings[bindingIndex];

            if (!binding.isPartOfComposite)
                return new RowKey(action.name, "");

            return new RowKey(action.name, binding.name);
        }
    }
}


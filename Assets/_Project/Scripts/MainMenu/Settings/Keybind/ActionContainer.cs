using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.MainMenu {
    public class ActionContainer : MonoBehaviour {
        [SerializeField] private ActionRow actionRowPrefab;
        [SerializeField] private Transform rowParent;

        private readonly Dictionary<RowKey, ActionRow> _rows = new();

        public void Initialize(
            InputActionMap actionMap,
            KeybindMenuController keybindMenuController
        ) {
            Clear();

            foreach (InputAction action in actionMap.actions) {
                BuildRows(action, keybindMenuController);
            }

            foreach (ActionRow row in _rows.Values) {
                row.Build();
            }
        }

        public void Refresh() {
            foreach (ActionRow row in _rows.Values) {
                row.Refresh();
            }
        }

        private void BuildRows(
            InputAction action,
            KeybindMenuController keybindMenuController
        ) {
            for (int bindingIndex = 0;
                 bindingIndex < action.bindings.Count;
                 bindingIndex++) {

                InputBinding binding = action.bindings[bindingIndex];

                // Composite roots such as "2D Vector" aren't actual
                // bindable controls, so don't create buttons for them.
                if (binding.isComposite)
                    continue;

                BindingDevice device = GetDevice(binding);

                RowKey key = GetRowKey(action, bindingIndex);

                if (!_rows.TryGetValue(key, out ActionRow row)) {
                    row = Instantiate(actionRowPrefab, rowParent);

                    row.Initialize(
                        action,
                        GetRowDisplayName(action, binding),
                        keybindMenuController
                    );

                    _rows.Add(key, row);
                }

                row.AddBinding(device, bindingIndex);
            }
        }

        private void Clear() {
            foreach (Transform child in rowParent) {
                Destroy(child.gameObject);
            }

            _rows.Clear();
        }

        private string GetRowDisplayName(
            InputAction action,
            InputBinding binding
        ) {
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

        private string FormatName(string value) {
            return string.IsNullOrEmpty(value)
                ? ""
                : char.ToUpper(value[0]) + value.Substring(1);
        }

        private BindingDevice GetDevice(InputBinding binding) {
            string path = binding.path;
            if (path.StartsWith(
                    "<Keyboard>",
                    StringComparison.OrdinalIgnoreCase
                )) {
                return BindingDevice.Keyboard;
            }

            if (path.StartsWith(
                    "<Mouse>",
                    StringComparison.OrdinalIgnoreCase
                )) {
                return BindingDevice.Mouse;
            }

            if (path.StartsWith(
                    "<Gamepad>",
                    StringComparison.OrdinalIgnoreCase
                )) {
                return BindingDevice.Gamepad;
            }

            throw new InvalidOperationException(
                $"Unsupported binding path '{path}' " +
                $"for binding '{binding.name}'."
            );
        }

        private RowKey GetRowKey(
            InputAction action,
            int bindingIndex
        ) {
            InputBinding binding = action.bindings[bindingIndex];

            if (!binding.isPartOfComposite)
                return new RowKey(action.name, "");

            return new RowKey(action.name, binding.name);
        }
    }
}
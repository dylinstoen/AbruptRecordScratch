using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace _Project.Scripts.MainMenu {
    public class ActionContainer : MonoBehaviour {
        [SerializeField] private ActionRow actionRowPrefab;
        [SerializeField] private Transform rowParent;
        public event Action Rebuilt;
        private readonly Dictionary<RowKey, ActionRow> _rows = new();

        public void Initialize(InputActionMap actionMap) {
            Clear();
            foreach(InputAction action in actionMap.actions) {
                BuildRows(action);
            }
            foreach(ActionRow row in _rows.Values) {
                row.Refresh();
            }
        }

        private void BuildRows(InputAction action) {
            for(int bindingIndex = 0; bindingIndex < action.bindings.Count; bindingIndex++) {
                InputBinding binding = action.bindings[bindingIndex];
                if (binding.isComposite)
                    continue;
                BindingDevice device = GetDevice(binding);
                RowKey key = GetRowKey(action, bindingIndex);
                if(!_rows.TryGetValue(key, out ActionRow row)) {
                    row = Instantiate(actionRowPrefab, rowParent);
                    row.Initialize(action, GetRowDisplayName(action, action.bindings[bindingIndex]));
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
            if(binding.groups.Contains("Gamepad")) {
                return BindingDevice.Gamepad;
            }
            if(binding.groups.Contains("Keyboard&Mouse")) {
                // Could be mouse or keyboard
                string path = binding.effectivePath;
                return path.StartsWith("<Mouse>") || path.StartsWith("<Pointer>") ? BindingDevice.Mouse : BindingDevice.Keyboard;
            }
            throw new InvalidOperationException("No Device associated with the binding");
        }

        private RowKey GetRowKey(InputAction action, int bindingIndex) {
            // action = move, jump, etc. BindingIndex = move[0] = 2d vector (keyboard), move[1] = w, move[2] = a, move[3] = s,..., move[5] = 2d vector (gamepad), move[6] = left stick up
            InputBinding binding = action.bindings[bindingIndex];
            if(!binding.isPartOfComposite) {
                return new RowKey(action.name, "", ""); // jump[0] = space, jump[1] = a
            }
            int compositeIndex = FindParentCompositeIndex(action, bindingIndex);
            InputBinding composite = action.bindings[compositeIndex];
            return new RowKey(action.name, composite.path, binding.name);
        }

        private int FindParentCompositeIndex(InputAction action, int startBindingIndex) {
            for (int i = startBindingIndex - 1; i >= 0; i--) {
                InputBinding binding = action.bindings[i];
                if(binding.isComposite)
                    return i;
                if (!binding.isPartOfComposite)
                    break;
            }
            throw new InvalidOperationException($"Binding '{action.name}/{action.bindings[startBindingIndex].name}' is part of a composite, " + $"but no parent composite exists.");
        }
    }
}


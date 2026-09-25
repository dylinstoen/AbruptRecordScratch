using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Project.Scripts.MainMenu {
    public sealed class InputBindingController : MonoBehaviour {
        [SerializeField] private InputActionAsset _actions;

        public InputActionAsset Actions => _actions;

        private string FilePath =>
            Path.Combine(
                Application.persistentDataPath,
                "input-bindings.json"
            );

        private void Awake() {
            Load();
        }

        public string CaptureCurrentOverrides() {
            return _actions.SaveBindingOverridesAsJson();
        }

        public void RestoreOverrides(string json) {
            _actions.RemoveAllBindingOverrides();

            if (!string.IsNullOrWhiteSpace(json)) {
                _actions.LoadBindingOverridesFromJson(json);
            }
        }

        public InputActionRebindingExtensions.RebindingOperation BeginRebind(
            InputAction action,
            int bindingIndex,
            Action onComplete,
            Action onCancel,
            float timeoutSeconds
        ) {
            action.Disable();

            InputBinding binding = action.bindings[bindingIndex];

            var operation = action
                .PerformInteractiveRebinding(bindingIndex)
                .WithTimeout(timeoutSeconds);

            // Use the ORIGINAL binding path, not effectivePath.
            //
            // Keyboard and Mouse now belong to the same Keyboard&Mouse
            // control scheme, but we can still restrict each individual
            // binding slot to its original device type.
            string originalPath = binding.path;

            if (originalPath.StartsWith(
                    "<Keyboard>",
                    StringComparison.OrdinalIgnoreCase
                )) {
                operation.WithControlsHavingToMatchPath("<Keyboard>");
            }
            else if (originalPath.StartsWith(
                         "<Mouse>",
                         StringComparison.OrdinalIgnoreCase
                     )) {
                operation.WithControlsHavingToMatchPath("<Mouse>");
            }
            else if (originalPath.StartsWith(
                         "<Gamepad>",
                         StringComparison.OrdinalIgnoreCase
                     )) {
                operation.WithControlsHavingToMatchPath("<Gamepad>");
            }

            operation
                .OnComplete(op => {
                    op.Dispose();
                    action.Enable();
                    onComplete?.Invoke();
                })
                .OnCancel(op => {
                    op.Dispose();
                    action.Enable();
                    onCancel?.Invoke();
                });

            operation.Start();

            return operation;
        }

        public void SaveCurrentOverrides() {
            string json = CaptureCurrentOverrides();
            File.WriteAllText(FilePath, json);
        }

        public void Load() {
            _actions.RemoveAllBindingOverrides();

            if (!File.Exists(FilePath))
                return;

            string json = File.ReadAllText(FilePath);

            if (!string.IsNullOrWhiteSpace(json)) {
                _actions.LoadBindingOverridesFromJson(json);
            }
        }

        public void RestoreDefaults() {
            _actions.RemoveAllBindingOverrides();
        }

        internal void RemoveBinding(InputAction action, int bindingIndex) {
            action.ApplyBindingOverride(
                bindingIndex,
                new InputBinding {
                    overridePath = ""
                }
            );
        }
    }
}
using System;
using System.IO;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

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

            if (binding.effectivePath.StartsWith("<Keyboard>")) {
                operation
                    .WithControlsHavingToMatchPath("<Keyboard>")
                    .WithControlsExcluding("<Mouse>")
                    .WithControlsExcluding("<Gamepad>");
            }
            else if (binding.effectivePath.StartsWith("<Mouse>")) {
                operation
                    .WithControlsHavingToMatchPath("<Mouse>")
                    .WithControlsExcluding("<Keyboard>")
                    .WithControlsExcluding("<Gamepad>");
            }
            else if (binding.effectivePath.StartsWith("<Gamepad>")) {
                operation
                    .WithControlsHavingToMatchPath("<Gamepad>")
                    .WithControlsExcluding("<Keyboard>")
                    .WithControlsExcluding("<Mouse>");
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
            action.ApplyBindingOverride(bindingIndex, new InputBinding { overridePath = ""});
        }
    }
}
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

        public InputActionRebindingExtensions.RebindingOperation BeginRebind(InputAction action, int bindingIndex, Action onComplete, Action onCancel) {
            action.Disable();
            return action.PerformInteractiveRebinding(bindingIndex)
                .OnComplete(operation => {
                    operation.Dispose();
                    action.Enable();
                    onComplete?.Invoke();
                })
                .OnCancel(operation => {
                    operation.Dispose();
                    action.Enable();
                    onCancel?.Invoke();
                })
                .Start();
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
    }
}
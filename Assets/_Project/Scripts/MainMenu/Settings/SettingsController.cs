using System.IO;
using UnityEngine;

namespace _Project.Scripts.MainMenu {
    public sealed class SettingsController : MonoBehaviour {

        [SerializeField] private InputBindingController _inputBindings;

        public SettingsData Saved { get; private set; }

        private string FilePath =>
            Path.Combine(
                Application.persistentDataPath,
                "settings.json"
            );

        private void Awake() {
            Load();
        }

        public SettingsSession CreateSession() {
            return new SettingsSession(this);
        }

        public SettingsData CreateEditingCopy() {
            return new SettingsData(Saved);
        }

        public void Load() {
            if (!File.Exists(FilePath)) {
                Saved = CreateDefaultSettings();

                WriteToDisk(Saved);
                ApplyToGame(Saved);
                return;
            }
            string json = File.ReadAllText(FilePath);

            SettingsData loaded = JsonUtility.FromJson<SettingsData>(json);

            Saved = loaded != null ? new SettingsData(loaded) : new SettingsData();

            ApplyToGame(Saved);
        }
        public SettingsData CreateDefaultSettings() {
            int width = Display.main.systemWidth;
            int height = Display.main.systemHeight;

            int refreshRate = 60; // Fallback

            foreach (Resolution resolution in Screen.resolutions) {
                if (resolution.width == width &&
                    resolution.height == height) {
                    refreshRate = Mathf.Max(
                        refreshRate,
                        Mathf.RoundToInt((float)resolution.refreshRateRatio.value));
                }
            }


            return new SettingsData {
                ResolutionWidth = width,
                ResolutionHeight = height,
                RefreshRate = refreshRate,
                WindowMode = false,
                Volume = 1f,
                VSync = true,
                InvertLook = false,
                LookSensitivity = 1f,
                VerticalFOV = 60f
            };
        }
        public void Save(SettingsData data) {
            if (data == null) {
                Debug.LogError("Cannot save null settings data.");
                return;
            }

            // Never keep a reference to the session's WorkingCopy.
            Saved = new SettingsData(data);

            WriteToDisk(Saved);
            ApplyToGame(Saved);
        }

        private void WriteToDisk(SettingsData data) {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(FilePath, json);
        }

        private void ApplyToGame(SettingsData data) {
            AudioListener.volume = data.Volume;
            QualitySettings.vSyncCount = data.VSync ? 1 : 0;
            
            

            FullScreenMode mode = data.WindowMode
                ? FullScreenMode.Windowed
                : FullScreenMode.FullScreenWindow;

            Screen.SetResolution(
                data.ResolutionWidth,
                data.ResolutionHeight,
                mode
            );


            // TODO:
            // Apply refresh rate
            // Apply FOV to camera
            // Apply sensitivity to character
            // Apply invert look to character
        }
    }
}
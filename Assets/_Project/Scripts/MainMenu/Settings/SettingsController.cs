using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace _Project.Scripts.MainMenu {
    public sealed class SettingsController : MonoBehaviour, ISettingsService {

        private readonly List<ISettingsReceiver> _receivers = new();
        public SettingsData Saved { get; private set; }

        private string FilePath =>
            Path.Combine(
                Application.persistentDataPath,
                "settings.json"
            );

        private void Awake() {
            Load();
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
                if (resolution.width != width || resolution.height != height) {
                    continue;
                }
                refreshRate = Mathf.Max(refreshRate, Mathf.RoundToInt((float)resolution.refreshRateRatio.value));
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

        public void Register(ISettingsReceiver receiver) {
            if (_receivers.Contains(receiver)) {
                return;
            }
            _receivers.Add(receiver);
            if (Saved != null) {
                receiver.ApplySettings(Saved);
            }
        }
        public void Unregister(ISettingsReceiver reciever) {
            _receivers.Remove(reciever);
        }

        private void WriteToDisk(SettingsData data) {
            string json = JsonUtility.ToJson(data, true);
            File.WriteAllText(FilePath, json);
        }

        private void ApplyToGame(SettingsData data) {
            ApplyAudio(data);
            ApplyVSync(data);
            ApplyDisplay(data);
            ApplyGameplaySettings(data);
        }

        private void ApplyAudio(SettingsData data) {
            AudioListener.volume = data.Volume;
        }
        private void ApplyVSync(SettingsData data) {
            QualitySettings.vSyncCount =
                data.VSync ? 1 : 0;
        }
        private void ApplyDisplay(SettingsData data) {
            if (data.WindowMode) {
                Screen.SetResolution(
                    data.ResolutionWidth,
                    data.ResolutionHeight,
                    FullScreenMode.Windowed
                );

                return;
            }

            RefreshRate refreshRate = FindRefreshRate(
                data.ResolutionWidth,
                data.ResolutionHeight,
                data.RefreshRate
            );

            Screen.SetResolution(
                data.ResolutionWidth,
                data.ResolutionHeight,
                FullScreenMode.ExclusiveFullScreen,
                refreshRate
            );
        }

        private RefreshRate FindRefreshRate(int width, int height, int desiredRefreshRate) {
            RefreshRate closest = new RefreshRate {
                numerator = (uint)desiredRefreshRate,
                denominator = 1
            };

            float closestDifference = float.MaxValue;

            foreach (Resolution resolution in Screen.resolutions) {
                if (resolution.width != width ||
                    resolution.height != height) {
                    continue;
                }

                float rate =
                    (float)resolution.refreshRateRatio.value;

                float difference =
                    Mathf.Abs(rate - desiredRefreshRate);

                if (difference >= closestDifference) {
                    continue;
                }

                closestDifference = difference;
                closest = resolution.refreshRateRatio;
            }

            return closest;
        }
        private void ApplyGameplaySettings(SettingsData data) {
            _receivers.RemoveAll(IsStale);
            // The copying of the data prevents edge case bugs where applying a setting causes applying another setting and therefore the array is modified as were trying to apply it.
            ISettingsReceiver[] receivers = _receivers.ToArray();
            foreach (ISettingsReceiver receiver in receivers) {
                receiver.ApplySettings(data);
            }
        }
        private static bool IsStale(ISettingsReceiver receiver) {
            if (receiver == null) {
                return true;
            }

            if (receiver is UnityEngine.Object unityObject) {
                return unityObject == null;
            }

            return false;
        }

    }
}
using System;
using System.Diagnostics;
using _Project.Scripts.GameRoot;

namespace _Project.Scripts.MainMenu {
    public sealed class SettingsSession {
        private readonly ISettingsService _settingService;
        private SettingsData _original;

        public SettingsData WorkingCopy { get; private set; }

        public event Action ResolutionChanged;

        public bool HasChanges =>
            !SettingsDataComparer.AreEqual(WorkingCopy, _original);

        public SettingsSession(ISettingsService settingService) {
            _settingService = settingService;

            _original = _settingService.CreateEditingCopy();
            WorkingCopy = new SettingsData(_original);
            
        }

        public float GetVerticalFOV() {
            return _settingService.GetVerticalFOV();
        }

        public void SetResolution(int width, int height) {
            if (WorkingCopy.ResolutionWidth == width &&
                WorkingCopy.ResolutionHeight == height) {
                return;
            }

            WorkingCopy.ResolutionWidth = width;
            WorkingCopy.ResolutionHeight = height;

            ResolutionChanged?.Invoke();
        }

        public void SetRefreshRate(int refreshRate) {
            WorkingCopy.RefreshRate = refreshRate;
        }

        public void Apply() {
            _settingService.Save(WorkingCopy);

            _original = _settingService.CreateEditingCopy();
            WorkingCopy = new SettingsData(_original);

            ResolutionChanged?.Invoke();
        }

        public void Discard() {
            WorkingCopy = new SettingsData(_original);

            ResolutionChanged?.Invoke();
        }

        public void RestoreDefaults() {
            WorkingCopy = _settingService.CreateDefaultSettings();

            ResolutionChanged?.Invoke();
        }
    }
}
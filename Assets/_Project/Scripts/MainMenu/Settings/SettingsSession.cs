using System;

namespace _Project.Scripts.MainMenu {
    public sealed class SettingsSession {
        private readonly SettingsController _controller;
        private SettingsData _original;

        public SettingsData WorkingCopy { get; private set; }

        public event Action ResolutionChanged;

        public bool HasChanges =>
            !SettingsDataComparer.AreEqual(WorkingCopy, _original);

        public SettingsSession(SettingsController controller) {
            _controller = controller;

            _original = controller.CreateEditingCopy();
            WorkingCopy = new SettingsData(_original);
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
            _controller.Save(WorkingCopy);

            _original = _controller.CreateEditingCopy();
            WorkingCopy = new SettingsData(_original);

            ResolutionChanged?.Invoke();
        }

        public void Discard() {
            WorkingCopy = new SettingsData(_original);

            ResolutionChanged?.Invoke();
        }

        public void RestoreDefaults() {
            WorkingCopy = _controller.CreateDefaultSettings();

            ResolutionChanged?.Invoke();
        }
    }
}
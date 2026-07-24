using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Utilities;

namespace _Project.Scripts.Gameplay.Interract {
    public sealed class StainPool : KeyedPool<StainType, StainInstance> {
        private bool _isPaused;
        private ILevelStateSource _levelStateSource;
        public void Initialize(ILevelStateSource levelStateSource) {
            _levelStateSource = levelStateSource;
            levelStateSource.StateChanged += OnStateChanged;
        }

        private void OnStateChanged(LevelState state) {
            SetPaused(state == LevelState.Paused);
        }
        public void SetPaused(bool paused) {
            if (_isPaused == paused)
                return;

            _isPaused = paused;

            foreach (var stain in ActiveInstances)
                stain.SetPaused(paused);
        }

        protected override void OnRented(StainInstance stain) {
            stain.SetPaused(_isPaused);
        }

        protected override void OnReturned(StainInstance stain) {
            // Nothing is currently required here.
            // The hook remains available for stain-specific cleanup.
        }

        private void OnDestroy() {
            if (_levelStateSource != null)
                _levelStateSource.StateChanged -= OnStateChanged;
        }
    }
}
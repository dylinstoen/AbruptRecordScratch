using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Utilities;

namespace _Project.Scripts.Gameplay {
    public sealed class SplatterPool
        : KeyedPool<SplatterType, SplatterInstance> {

        private bool _isPaused;
        private ILevelStateSource _levelStateSource;

        public void Initialize(ILevelStateSource levelStateSource) {
            _levelStateSource = levelStateSource;
            _levelStateSource.StateChanged += OnStateChanged;

            SetPaused(
                _levelStateSource.CurrentState == LevelState.Paused
            );
        }

        private void OnStateChanged(LevelState state) {
            SetPaused(state == LevelState.Paused);
        }

        public void SetPaused(bool paused) {
            if (_isPaused == paused)
                return;

            _isPaused = paused;

            foreach (SplatterInstance splatter in ActiveInstances) {
                splatter.SetPaused(paused);
            }
        }

        protected override void OnRented(SplatterInstance splatter) {
            splatter.SetPaused(_isPaused);
        }

        protected override void OnReturned(SplatterInstance splatter) {
            // Optional cleanup if ForceRecycle is not already
            // called by KeyedPool when an instance is returned.
        }

        private void OnDestroy() {
            if (_levelStateSource != null) {
                _levelStateSource.StateChanged -= OnStateChanged;
            }
        }
    }
}
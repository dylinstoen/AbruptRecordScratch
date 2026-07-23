using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Input;
using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class PlayerPause : MonoBehaviour {
        private ILevelController levelController;
        private ILevelStateSource levelStateSource;
        private IIntentSource _intent;
        public void Initialize(ILevelController levelController, ILevelStateSource levelStateSource, IIntentSource intent) {
            this.levelController = levelController;
            this.levelStateSource = levelStateSource;
            this._intent = intent;
        }
        private void Update() {
            if (levelController == null || levelStateSource == null || _intent == null) {
                Debug.LogError("Level Controller or Intent Source is not initialized in PlayerPause.");
                return;
            }
            if (_intent.Current.Pause) {
                if (levelStateSource.CurrentState == LevelState.Playing) {
                    levelController.TogglePauseLevel(true);
                }
                else if (levelStateSource.CurrentState == LevelState.Paused) {
                    levelController.TogglePauseLevel(false);
                }
            }
        }
    }
}

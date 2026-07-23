using UnityEngine;

using System;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Core.Level.Interface;

namespace _Project.Scripts.Gameplay {
    public class LevelController : MonoBehaviour, ILevelStateSource, ILevelController {
        private LevelState state;

        public LevelState CurrentState => state;

        public bool IsGameplayActive => state == LevelState.Playing;

        public event Action<LevelState> StateChanged;
        public event Action LevelCompleted; // Event to notify when the level is completed, passing the score as an integer

        public void StartLevel () {
            SetState(LevelState.Playing);
        }
        public void CompleteLevel() {
            SetState(LevelState.Completed);
        }
        public void TogglePauseLevel (bool pausing) {
            if (pausing) {
                SetState(LevelState.Paused);
            }
            else {
                SetState(LevelState.Playing);
            }

        }

        private void SetState(LevelState newState) {
            if (state == newState) return;
            switch(newState) {
                case LevelState.Completed:
                    LevelCompleted?.Invoke();
                    break;
            }
            state = newState;
            StateChanged?.Invoke(state);
        }
    }
}



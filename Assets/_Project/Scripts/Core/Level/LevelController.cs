using UnityEngine;

using System;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Core.Level.Interface;

namespace _Project.Scripts.Gameplay {
    public class LevelController : MonoBehaviour, ILevelStateSource, ILevelController {
        public LevelState State { get; private set; }

        public bool IsGameplayActive => State == LevelState.Playing;

        public event Action<LevelState> StateChanged;
        public event Action LevelCompleted; // Event to notify when the level is completed, passing the score as an integer

        public void StartLevel () {
            SetState(LevelState.Playing);
        }
        public void CompleteLevel() {
            SetState(LevelState.Completed);
        }

        private void SetState(LevelState newState) {
            if (State == newState) return;
            switch(newState) {
                case LevelState.Completed:
                    LevelCompleted?.Invoke();
                    break;
            }
            State = newState;
            StateChanged?.Invoke(State);
        }
    }
}



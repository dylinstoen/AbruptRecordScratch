using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay.Enums;
using System;
using UnityEngine;

public class LevelController :
    MonoBehaviour,
    ILevelStateSource,
    ILevelController {

    private LevelState state;

    public LevelState CurrentState => state;

    public bool IsGameplayActive =>
        state == LevelState.Playing;

    public event Action<LevelState> StateChanged;
    public event Action LevelCompleted;

    public void StartLevel() {
        SetState(LevelState.Playing);
    }

    public void CompleteLevel() {
        SetState(LevelState.Completed);
    }

    public void PlayerDied() {
        SetState(LevelState.Dead);
    }

    public void TogglePauseLevel(bool pausing) {
        if (pausing) {
            // Extra defensive check.
            if (state != LevelState.Playing)
                return;

            SetState(LevelState.Paused);
        }
        else {
            // Only resume from pause.
            if (state != LevelState.Paused)
                return;

            SetState(LevelState.Playing);
        }
    }

    private void SetState(LevelState newState) {
        if (state == newState)
            return;

        state = newState;

        if (newState == LevelState.Completed) {
            LevelCompleted?.Invoke();
        }

        StateChanged?.Invoke(state);
    }
}
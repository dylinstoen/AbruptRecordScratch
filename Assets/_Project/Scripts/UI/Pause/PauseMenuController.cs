using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Input;
using _Project.Scripts.UI.Navigation;
using System;
using UnityEngine;

namespace _Project.Scripts.UI.Pause {
    public class PauseMenuController : MonoBehaviour {
        [SerializeField] private MenuNavigationController navigation;
        [SerializeField] private MenuPage pausePage;

        private ILevelController _levelController;
        private ILevelStateSource _levelStateSource;
        private IInputModeService _inputMode;
        private GameplayPauseInput _pauseInput;

        private bool _pauseMenuOpen;

        public void Initialize(
            ILevelController levelController,
            ILevelStateSource levelStateSource,
            IInputModeService inputMode,
            GameplayPauseInput pauseInput
        ) {
            Unsubscribe();

            _levelController =
                levelController ??
                throw new ArgumentNullException(nameof(levelController));

            _levelStateSource =
                levelStateSource ??
                throw new ArgumentNullException(nameof(levelStateSource));

            _inputMode =
                inputMode ??
                throw new ArgumentNullException(nameof(inputMode));

            _pauseInput =
                pauseInput ??
                throw new ArgumentNullException(nameof(pauseInput));

            _pauseMenuOpen = false;

            navigation.Close();

            Subscribe();
        }

        private void OnDestroy() {
            Unsubscribe();

            Time.timeScale = 1f;
        }

        private void Subscribe() {
            if (_pauseInput != null)
                _pauseInput.PauseRequested += OnPauseRequested;

            if (_levelStateSource != null)
                _levelStateSource.StateChanged += OnLevelStateChanged;
        }

        private void Unsubscribe() {
            if (_pauseInput != null)
                _pauseInput.PauseRequested -= OnPauseRequested;

            if (_levelStateSource != null)
                _levelStateSource.StateChanged -= OnLevelStateChanged;
        }

        private void OnPauseRequested() {
            if (_levelStateSource.CurrentState != LevelState.Playing)
                return;

            _levelController.TogglePauseLevel(true);
        }

        private void OnLevelStateChanged(LevelState state) {
            switch (state) {
                case LevelState.Paused:
                    OpenPauseMenu();
                    break;

                case LevelState.Playing:
                    if (_pauseMenuOpen)
                        ClosePauseMenu();
                    break;
            }
        }

        private void OpenPauseMenu() {
            if (_pauseMenuOpen)
                return;

            _pauseMenuOpen = true;

            Time.timeScale = 0f;

            _inputMode.SetUI();

            navigation.OpenRoot(pausePage);
        }

        private void ClosePauseMenu() {
            if (!_pauseMenuOpen)
                return;

            _pauseMenuOpen = false;

            navigation.Close();

            Time.timeScale = 1f;

            _inputMode.SetGameplay();
        }

        public void Resume() {
            if (_levelStateSource.CurrentState != LevelState.Paused)
                return;

            _levelController.TogglePauseLevel(false);
        }
    }
}
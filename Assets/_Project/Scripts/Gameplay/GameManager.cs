using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Actors;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Input;
using _Project.Scripts.UI.DeathScreen;
using UnityEngine;
using UnityEngine.SceneManagement;
using IDisposable = _Project.Scripts.Actors.IDisposable;

namespace _Project.Scripts.Gameplay {
    public class GameManager : MonoBehaviour, IDisposable {
        [SerializeField] private float deathScreenCoolDown = 5f;
        private IDeathEvents _playerDeathEvents;
        private GameState _gameState;
        private IDeathScreen _deathScreen;
        private IInputModeService _inputModeService;

        public void BindPlayerHealthEvents(IDeathEvents playerDeathEvents) {
            _gameState = GameState.Alive;
            _inputModeService.SetGameplay();
            _playerDeathEvents = playerDeathEvents;
            _playerDeathEvents.Died += OnPlayerDied;
        }
        

        private void OnPlayerDied() {
            _gameState = GameState.Dead;
            _playerDeathEvents.Died -= OnPlayerDied;
            StartCoroutine(ShowDeathScreen());
            // TODO: Wait for x seconds
        }

        private IEnumerator ShowDeathScreen() {
            yield return new WaitForSeconds(deathScreenCoolDown);
            _deathScreen.Show();
            _inputModeService.SetDead();
        }

        private void OnDestroy() {
            Dispose();
        }

        public void Dispose() {
            if (_playerDeathEvents != null) {
                _playerDeathEvents.Died -= OnPlayerDied;
            }

            if (_inputModeService != null) {
                _inputModeService.DeathUIIInputEvent.ContinueRequested -= ContinueRequest;
            }
        }

        public void BindInputModeService(IInputModeService inputModeService) {
            _inputModeService = inputModeService;
        }
        
        public void BindDeathScreen(IDeathScreen deathScreen) {
            _deathScreen = deathScreen;
            _inputModeService.DeathUIIInputEvent.ContinueRequested += ContinueRequest;
        }

        private void ContinueRequest() {
            _deathScreen.Hide();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
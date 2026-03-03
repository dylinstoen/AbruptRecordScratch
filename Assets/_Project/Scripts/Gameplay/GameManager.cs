using System;
using System.Collections;
using System.Collections.Generic;
using _Project.Scripts.Actors;
using _Project.Scripts.Gameplay.Enums;
using _Project.Scripts.Input;
using _Project.Scripts.UI.DeathScreen;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Project.Scripts.Gameplay {
    public class GameManager : MonoBehaviour {
     [SerializeField] private float deathScreenDelay = 5f;

    private IDeathEvents _deathEvents;
    private IDeathScreen _deathScreen;
    private IInputModeService _inputMode;
    private IDeathUIIInputEvent _deathUiiInputEvent;

    private Coroutine _deathRoutine;
    private bool _isDead;

    public void Initialize(IDeathEvents deathEvents, IDeathScreen deathScreen, IInputModeService inputMode, IDeathUIIInputEvent deathUiiInputEvent) {
        Unsubscribe();
        _deathUiiInputEvent  = deathUiiInputEvent ?? throw new ArgumentNullException(nameof(deathUiiInputEvent));
        _deathEvents  = deathEvents  ?? throw new ArgumentNullException(nameof(deathEvents));
        _deathScreen  = deathScreen  ?? throw new ArgumentNullException(nameof(deathScreen));
        _inputMode    = inputMode    ?? throw new ArgumentNullException(nameof(inputMode));

        _isDead = false;

        _deathScreen.Hide();
        _inputMode.SetGameplay();

        Subscribe();
    }

    private void OnEnable()
    {
        Subscribe();
    }

    private void OnDisable()
    {
        Unsubscribe();
        StopDeathRoutine();
    }

    private void OnDestroy()
    {
        Unsubscribe();
    }

    private void Subscribe()
    {
        if (_deathEvents != null)
            _deathEvents.Died += OnPlayerDied;

        if (_inputMode != null)
            _deathUiiInputEvent.ContinueRequested += OnContinueRequested;
    }

    private void Unsubscribe()
    {
        if (_deathEvents != null)
            _deathEvents.Died -= OnPlayerDied;

        if (_inputMode != null)
            _deathUiiInputEvent.ContinueRequested -= OnContinueRequested;
    }

    private void OnPlayerDied()
    {
        if (_isDead) return;
        _isDead = true;

        StopDeathRoutine();
        _deathRoutine = StartCoroutine(DeathSequence());
    }

    private IEnumerator DeathSequence()
    {
        yield return new WaitForSeconds(deathScreenDelay);
        _inputMode.SetDead();
        _deathScreen.Show();
    }

    private void StopDeathRoutine()
    {
        if (_deathRoutine != null)
        {
            StopCoroutine(_deathRoutine);
            _deathRoutine = null;
        }
    }

    private void OnContinueRequested()
    {
        if (!_isDead) return;

        _deathScreen.Hide();
        ReloadActiveScene();
    }

    private static void ReloadActiveScene()
    {
        var scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.buildIndex);
    }
    }
}
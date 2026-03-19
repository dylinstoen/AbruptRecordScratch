using System;
using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Core;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class DoorInteractable : MonoBehaviour, IInteractable {
    private const float CloseThreshold = 5f;

    [Header("References")]
    [SerializeField] private Animator animator;
    [SerializeField] private Transform doorAnchor;
    [SerializeField] private Transform knobAnchor;

    [Header("Audio")]
    [SerializeField] private AudioCue doorOpenStartedSound;
    [SerializeField] private AudioCue doorClosedSound;
    [SerializeField] private AudioCue doorMovingOpenSound;
    [SerializeField] private AudioCue doorMovingCloseSound;

    [Header("Door")]
    [SerializeField] private Vector3 openAngle = new Vector3(0f, 90f, 0f);
    [SerializeField] private float doorOpenDelay = 0.10f;
    [SerializeField] private float speed = 10f;

    [Header("Knob")]
    [SerializeField] private string unlockAnimationTrigger = "Unlock";

    [Header("Interaction Cue")]
    [SerializeField] private string promptText;
    [SerializeField] private Transform promptAnchor;
    [SerializeField] private OutlineHighlightable highlightObj;

    private Quaternion _closedRotation;
    private Quaternion _openRotation;

    private bool _isOpening;
    private bool _isFullyClosed = true;
    private float _openDelayRemaining;

    public bool CanInteract() => true;

    private void Start() {
        _closedRotation = doorAnchor.rotation;
        _openRotation = _closedRotation * Quaternion.Euler(openAngle);
    }

    private void Update() {
        TickDelay();
        if (_openDelayRemaining > 0f)
            return;

        RotateDoor();
        HandleFullyClosedReached();
    }

    public InteractionCue GetCue() {
        return new InteractionCue(promptText, promptAnchor, highlightObj);
    }

    public void Interact() {
        _isOpening = !_isOpening;

        if (_isOpening)
            BeginOpening();
        else
            BeginClosing();
    }

    private void BeginOpening() {
        if (IsClosed()) {
            PlayAudio(doorOpenStartedSound);
            animator.SetTrigger(unlockAnimationTrigger);
            _openDelayRemaining = doorOpenDelay;
        }

        PlayAudio(doorMovingOpenSound);
        _isFullyClosed = false;
    }

    private void BeginClosing() {
        PlayAudio(doorMovingCloseSound);
    }

    private void TickDelay() {
        if (_openDelayRemaining <= 0f)
            return;

        _openDelayRemaining -= Time.deltaTime;
        if (_openDelayRemaining < 0f)
            _openDelayRemaining = 0f;
    }

    private void RotateDoor() {
        Quaternion targetRotation = _isOpening ? _openRotation : _closedRotation;
        doorAnchor.rotation = Quaternion.Slerp(
            doorAnchor.rotation,
            targetRotation,
            Time.deltaTime * speed
        );
    }

    private void HandleFullyClosedReached() {
        if (_isOpening)
            return;

        if (!_isFullyClosed && IsClosed()) {
            PlayAudio(doorClosedSound);
            _isFullyClosed = true;
        }
    }

    private bool IsClosed() {
        return Quaternion.Angle(doorAnchor.rotation, _closedRotation) <= CloseThreshold;
    }

    private void PlayAudio(AudioCue cue) {
        if (!cue)
            return;

        SceneServiceLocator.Current.Audio.Play3D(transform.position, transform.rotation, cue);
    }
    }
}
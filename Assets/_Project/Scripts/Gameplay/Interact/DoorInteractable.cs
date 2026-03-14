using System;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class DoorInteractable : MonoBehaviour, IInteractable {
        [SerializeField] private Animator animator;
        [Header("Door")]
        [SerializeField] private Transform doorAnchor;
        [SerializeField] private Vector3 openAngle = new Vector3(0,90f,0);
        [SerializeField] private float doorOpenDelay = 0.10f;
        [SerializeField] private float speed = 10f;
        [Header("Knob")] 
        [SerializeField] private Transform knobAnchor;
        [SerializeField] private string unlockAnimationTrigger = "Unlock";
        [Header("Interaction Cue")]
        [SerializeField] private string promptText;
        [SerializeField] private Transform promptAnchor;
        [SerializeField] private OutlineHighlightable highlightObj;

        public bool CanInteract() => true;

        private float _closeThreshold = 5f;


        private bool _isOpening;
        private bool _wasOpeningLastFrame;
        private Quaternion _openRotation;
        private Quaternion _closeRotation;
        private float _currentTime = 0f;

        private void Start() {
            _closeRotation = doorAnchor.rotation;
            _openRotation = Quaternion.Euler(openAngle);
            _wasOpeningLastFrame = false;
        }

        private void Update() {
            if (_currentTime > 0) {
                _currentTime -= Time.deltaTime;
                return;
            }
            if (IsClosed() && _isOpening && !_wasOpeningLastFrame) {
                animator.SetTrigger(unlockAnimationTrigger);
                _currentTime = doorOpenDelay;
            }
            else {
                Quaternion target = _isOpening ? _openRotation : _closeRotation;
                doorAnchor.rotation = Quaternion.Slerp(doorAnchor.rotation, target, Time.deltaTime * speed); 
            }
            _wasOpeningLastFrame = _isOpening;
        }

        private bool IsClosed() {
            return Quaternion.Angle(doorAnchor.rotation, _closeRotation) <= _closeThreshold;
        }

        public InteractionCue GetCue() {
            return new InteractionCue(promptText, promptAnchor, highlightObj);    
        }

        public void Interact() {
            _isOpening = !_isOpening;
        }
    }
}
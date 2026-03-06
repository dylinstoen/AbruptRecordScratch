using System;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class DoorInteractable : MonoBehaviour, IInteractable {
        [SerializeField] Transform doorAnchor;
        [SerializeField] Vector3 openAngle = new Vector3(0,90f,0);
        [SerializeField] private float speed = 10f;
        [Header("Interaction Cue")]
        [SerializeField] string promptText;
        [SerializeField] Transform promptAnchor;
        [SerializeField] OutlineHighlightable highlightObj;

        public bool CanInteract() => true;


        private bool _isOpen;
        private Quaternion _openRotation;
        private Quaternion _closeRotation;

        private void Start() {
            _closeRotation = doorAnchor.rotation;
            _openRotation = Quaternion.Euler(openAngle);
        }

        private void Update() {
            Quaternion target = _isOpen ? _openRotation : _closeRotation;
            doorAnchor.rotation = Quaternion.Slerp(doorAnchor.rotation, target, Time.deltaTime * speed);
        }

        public InteractionCue GetCue() {
            return new InteractionCue(promptText, promptAnchor, highlightObj);    
        }

        public void Interact() {
            Debug.Log("Door Opened");
            _isOpen = !_isOpen;
            // TODO: Make Door anchor move to the opposite position
        }

       
    }
}
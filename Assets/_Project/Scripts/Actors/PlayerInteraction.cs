using System;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Input;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class PlayerInteraction : MonoBehaviour {
        // 2 parts of the interaction
        // 1. detecting interactable objects nearby (displaying a prompt for the nearest one)
        // 2. pressing e grabs the nearest interactable and runs that objects interact code
        private IInteractionPresenter _interactionPresenter;
        [SerializeField] private LayerMask mask;
        private IIntentSource _intentSource;
        private IAimRaySource _aimRaySource;
        private IInteractable _focused;
        [SerializeField] private float radius = 0.15f;
        [SerializeField] private float range = 3f;
        public void Initialize(IInteractionPresenter interactionPresenter, IIntentSource intentSource, IAimRaySource aimRaySource) {
            _interactionPresenter = interactionPresenter;
            _intentSource = intentSource;
            _aimRaySource = aimRaySource;
            
        }

        private void Update() {
            
            // 1. Show interactable
            IInteractable newTarget = FindBestTarget();
            if (!ReferenceEquals(newTarget, _focused)) {
                _focused = newTarget;
                if (_focused != null) {
                    _interactionPresenter.Show(_focused.GetCue());
                }
                else {
                    _interactionPresenter.Hide();
                }
            }
            else if (_focused != null){
                _interactionPresenter.Show(_focused.GetCue());
            }
            if (_focused != null && _intentSource.Current.Interact) {
                // 2. Interact
                if (_focused.CanInteract()) {
                    _focused.Interact();
                }
            }
        }
        private IInteractable FindBestTarget() {
            if (Physics.Raycast(transform.position, _aimRaySource.GetAimRay().direction, out var hit, range, mask, QueryTriggerInteraction.Ignore)) {
                return hit.collider.GetComponentInParent<IInteractable>();
            }
            return null;
        }

        private void OnDrawGizmos() {

            Vector3 start = _aimRaySource.GetAimRay().origin;
            Vector3 end   = start + _aimRaySource.GetAimRay().direction * range;

            Gizmos.DrawWireSphere(start, radius);
            Gizmos.DrawLine(start, end);
            Gizmos.DrawWireSphere(end, radius);
        }
    }
}
using _Project.Scripts.Gameplay.Structs;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class InteractionPresenter : MonoBehaviour, IInteractionPresenter {
        [SerializeField] private Camera cam;
        [SerializeField] private TMP_Text promptText;
        [SerializeField] private Vector3 offset = new Vector3(0, 1.5f, 0);
        [SerializeField] private GameObject promptRoot;
        private Transform _target;
        
        private IHighlightable _currentHighlight;
        
        void Awake() {
            promptRoot.SetActive(false);
        }
        
        void LateUpdate() {
            if (!_target) return;
            transform.position = _target.position + offset;
            transform.forward = cam.transform.forward;
        }
        
        public void Hide() {
            if (promptRoot) {
                promptRoot.SetActive(false);
                promptRoot.transform.position = transform.position;
                _target = null;
            }
                
            _currentHighlight?.SetHighlight(false);
            _currentHighlight = null;
        }

        public void Show(InteractionCue cue) {
            if (promptRoot) {
                bool showPrompt = cue.HasPrompt;
                promptRoot.SetActive(showPrompt);
                if (showPrompt && promptText) {
                    promptText.text = cue.Prompt;
                }
                _target = cue.PromptAnchor;
            }
            if (cue.HasHighlight && _currentHighlight != cue.Highlight) {
                _currentHighlight?.SetHighlight(false);
                _currentHighlight = cue.Highlight;
                _currentHighlight?.SetHighlight(true);
            }
            // TODO: Hold UI
        }
    }
}
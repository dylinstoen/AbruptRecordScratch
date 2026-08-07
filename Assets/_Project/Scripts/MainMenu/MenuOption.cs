using UnityEngine;
using UnityEngine.EventSystems;

namespace _Project.Scripts.MainMenu {
    public sealed class MenuOption : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        ISelectHandler,
        IDeselectHandler {

        [SerializeField] private GameObject highlightVisual;

        private MenuInputModeTracker _inputModeTracker;
        private MenuPage _owner;

        private bool _isHovered;
        private bool _isSelected;

        public GameObject GameObject => gameObject;

        public void Initialize(
            MenuPage owner,
            MenuInputModeTracker inputModeTracker) {

            _owner = owner;
            _inputModeTracker = inputModeTracker;

            _inputModeTracker.ModeChanged += OnInputModeChanged;

            _isHovered = false;
            _isSelected = false;

            RefreshVisual();
        }

        public void ResetVisualState() {
            _isHovered = false;
            _isSelected = false;
            RefreshVisual();
        }

        public void RefreshVisual() {
            if (_inputModeTracker == null)
                return;

            bool shouldHighlight = _inputModeTracker.CurrentMode switch {
                MenuInputMode.Mouse => _isHovered,
                MenuInputMode.Controller => _isSelected,
                _ => false
            };

            highlightVisual.SetActive(shouldHighlight);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            _isHovered = true;
            RefreshVisual();
        }

        public void OnPointerExit(PointerEventData eventData) {
            _isHovered = false;
            RefreshVisual();
        }

        public void OnSelect(BaseEventData eventData) {
            
            _isSelected = true;
            _owner.RememberSelection(this);
            RefreshVisual();
        }

        public void OnDeselect(BaseEventData eventData) {
            _isSelected = false;
            RefreshVisual();
        }

        private void OnInputModeChanged(MenuInputMode mode) {
            RefreshVisual();
        }

        private void OnDestroy() {
            if (_inputModeTracker != null)
                _inputModeTracker.ModeChanged -= OnInputModeChanged;
        }
    }
}
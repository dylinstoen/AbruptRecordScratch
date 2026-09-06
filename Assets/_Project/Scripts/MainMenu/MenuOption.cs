using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.Scripts.MainMenu {
    public sealed class MenuOption : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        ISelectHandler,
        IDeselectHandler {

        [SerializeField] private GameObject highlightVisual;
        [SerializeField] private Selectable selectable;

        private MenuInputModeTracker _inputModeTracker;
        private MenuPage _owner;

        private bool _isHovered;
        private bool _isSelected;
        private bool _isInteractable = true;

        public GameObject GameObject => gameObject;
        public bool IsInteractable => _isInteractable;

        public void Initialize(
            MenuPage owner,
            MenuInputModeTracker inputModeTracker
        ) {
            _owner = owner;
            _inputModeTracker = inputModeTracker;

            _inputModeTracker.ModeChanged += OnInputModeChanged;

            _isHovered = false;
            _isSelected = false;

            RefreshVisual();
        }

        public void SetInteractable(bool isInteractable) {
            _isInteractable = isInteractable;

            if (selectable != null) {
                selectable.interactable = isInteractable;
            }

            if (!isInteractable) {
                _isHovered = false;
                _isSelected = false;
            }

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

            if (!_isInteractable) {
                highlightVisual.SetActive(false);
                return;
            }

            bool shouldHighlight =
                _inputModeTracker.CurrentMode switch {
                    MenuInputMode.Mouse => _isHovered,
                    MenuInputMode.Controller => _isSelected,
                    _ => false
                };

            highlightVisual.SetActive(shouldHighlight);
        }

        public void OnPointerEnter(PointerEventData eventData) {
            if (!_isInteractable)
                return;

            _isHovered = true;

            RefreshVisual();
        }

        public void OnPointerExit(PointerEventData eventData) {
            _isHovered = false;

            RefreshVisual();
        }

        public void OnSelect(BaseEventData eventData) {
            if (!_isInteractable)
                return;

            _isSelected = true;

            _owner?.RememberSelection(this);

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
            if (_inputModeTracker != null) {
                _inputModeTracker.ModeChanged -=
                    OnInputModeChanged;
            }
        }
    }
}
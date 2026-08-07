using _Project.Scripts.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.MainMenu {

    public class VerticalFOVSlider : MonoBehaviour, ISettingsControl {
        [SerializeField] private Slider slider;
        private SettingsSession _currentSession;
        public void Initialize(SettingsSession session) {
            _currentSession = session;
        }

        public void Refresh() {
            slider.value = _currentSession.WorkingCopy.VerticalFOV;
        }

        public void OnSliderValueChanged(float value) {
            _currentSession.WorkingCopy.Volume = value;
        }
    }
}


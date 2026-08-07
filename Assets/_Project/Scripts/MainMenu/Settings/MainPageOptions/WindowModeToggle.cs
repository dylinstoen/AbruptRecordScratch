using _Project.Scripts.MainMenu;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.MainMenu {
    public class WindowModeToggle : MonoBehaviour, ISettingsControl {

        [SerializeField] private Toggle toggle;

        private SettingsSession currentSession;
        public void Initialize(SettingsSession session) {
            currentSession = session;
        }

        public void Refresh() {
            toggle.SetIsOnWithoutNotify(currentSession.WorkingCopy.InvertLook);
        }
        public void OnToggleChange(bool value) {
            if (currentSession == null)
                return;
            currentSession.WorkingCopy.WindowMode = value;
        }
    }
}



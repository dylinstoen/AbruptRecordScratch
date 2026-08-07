using _Project.Scripts.MainMenu;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.UI;


public sealed class InvertLookToggle : MonoBehaviour, ISettingsControl
{
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
        currentSession.WorkingCopy.InvertLook = value;
    }

}

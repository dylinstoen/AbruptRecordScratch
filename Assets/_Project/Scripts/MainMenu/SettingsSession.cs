using UnityEngine;

public sealed class SettingsSession {
    public SettingsData WorkingCopy { get; }
    private SettingsController controller;

    public SettingsSession(SettingsController controller) {
        this.controller = controller;
        this.WorkingCopy = controller.CreateEditingCopy();
    }
    public void Apply() {
        controller.Save(WorkingCopy);
    }
    public void Discard() {
        // Nothing to do. Just close the menu.
    }
    public bool HasChanges => !Equals(WorkingCopy);

    private bool Equals(SettingsData other) {
        SettingsData current = controller.Saved;

        return
            current.InvertLook == other.InvertLook &&
            current.LookSensitivity == other.LookSensitivity &&
            current.VerticalFOV == other.VerticalFOV &&
            current.Resolution == other.Resolution &&
            current.WindowMode == other.WindowMode &&
            current.Volume == other.Volume &&
            current.VSync == other.VSync;
            
    }
}

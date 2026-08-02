using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public sealed class SettingsData {
    public bool InvertLook = false;
    public float LookSensitivity = 1f;
    public float VerticalFOV = 60f;
    public string Resolution = "1920x1080";
    public bool WindowMode = false;
    public float Volume = 1f;
    public bool VSync = true;

    public SettingsData() {
    }

    public SettingsData(SettingsData other) {
        InvertLook = other.InvertLook;
        LookSensitivity = other.LookSensitivity;
        VerticalFOV = other.VerticalFOV;
        Resolution = other.Resolution;
        WindowMode = other.WindowMode;
        Volume = other.Volume;
        VSync = other.VSync;
    }
}

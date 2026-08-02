using UnityEngine;
using System.IO;

public sealed class SettingsController : MonoBehaviour {
    public SettingsData Saved { get; private set; }

    private string FilePath =>
        Path.Combine(Application.persistentDataPath, "settings.json");

    private void Awake() {
        Debug.Log(Application.persistentDataPath);
        Load();
    }

    public void Load() {
        if (!File.Exists(FilePath)) {
            Saved = new SettingsData();
            Save(Saved);
            return;
        }

        string json = File.ReadAllText(FilePath);

        Saved = JsonUtility.FromJson<SettingsData>(json);
        Saved ??= new SettingsData();

        Apply(Saved);
    }

    public void Save(SettingsData data) {
        Saved = data;

        string json = JsonUtility.ToJson(Saved, true);

        File.WriteAllText(FilePath, json);

        Apply(Saved);
    }

    public SettingsData CreateEditingCopy() {
        return new SettingsData(Saved);
    }

    private void ApplyResolution(SettingsData data) {
        string[] parts = data.Resolution.Split('x');

        if (parts.Length != 2)
            return;

        if (!int.TryParse(parts[0], out int width))
            return;

        if (!int.TryParse(parts[1], out int height))
            return;

        FullScreenMode mode = data.WindowMode
            ? FullScreenMode.Windowed
            : FullScreenMode.FullScreenWindow;

        Screen.SetResolution(width, height, mode);
    }

    private void Apply(SettingsData data) {
        AudioListener.volume = data.Volume;
        QualitySettings.vSyncCount = data.VSync ? 1 : 0;
        ApplyResolution(data);

        // TODO:
        // Apply FOV to camera
        // Apply sensitivity to character
        // Apply invert look to character
    }


}

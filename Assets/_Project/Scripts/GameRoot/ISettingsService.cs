namespace _Project.Scripts.GameRoot {
    public interface ISettingsService {
        void Register(ISettingsReceiver receiver);
        void Unregister(ISettingsReceiver receiver);
        void Save(SettingsData data);
        float GetVerticalFOV();
        SettingsData CreateEditingCopy();
        SettingsData CreateDefaultSettings();
    }
}
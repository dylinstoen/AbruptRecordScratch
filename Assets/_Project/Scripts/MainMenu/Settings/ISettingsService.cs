namespace _Project.Scripts.MainMenu {
    public interface ISettingsService {
        SettingsData Saved { get; }

        void Register(ISettingsReceiver receiver);
        void Unregister(ISettingsReceiver receiver);
    }
}
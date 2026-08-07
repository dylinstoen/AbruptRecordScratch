using UnityEngine;

namespace _Project.Scripts.MainMenu {

    public interface ISettingsControl {
        void Initialize(SettingsSession session);
        void Refresh();
    }
}


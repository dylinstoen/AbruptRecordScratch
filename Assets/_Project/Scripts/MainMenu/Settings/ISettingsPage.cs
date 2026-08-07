using UnityEngine;

namespace _Project.Scripts.MainMenu {
    public interface ISettingsPage {
        public MenuPage ThisMenuPage { get; }
        void BindSession(SettingsSession? session);
    }
}


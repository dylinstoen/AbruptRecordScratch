using UnityEngine;

namespace _Project.Scripts.MainMenu {
    public interface IKeybindPage {
        MenuPage ThisMenuPage { get; }

        void BindSession(KeybindSession session);
    }
}
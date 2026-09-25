using UnityEngine;
using _Project.Scripts.UI.Navigation;

namespace _Project.Scripts.MainMenu {
    public interface IKeybindPage {
        public MenuPage ThisMenuPage { get; }
        void BindSession(KeybindSession session);
    }
}
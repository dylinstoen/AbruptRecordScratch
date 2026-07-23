using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Core.Level.Interface {
    public interface ILevelController {
        public void StartLevel();
        public void CompleteLevel();
        public void TogglePauseLevel(bool pausing);
    }
}


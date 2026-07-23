using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.UI.Pause {
    public class PauseHud : MonoBehaviour {
        [SerializeField] private GameObject pauseHud;

        private ILevelStateSource levelStateSource;

        public void Initialize(ILevelStateSource levelStateSource) {
            this.levelStateSource = levelStateSource;
            this.levelStateSource.StateChanged += StateChanged;
        }

        private void StateChanged(LevelState levelState) {
            pauseHud.SetActive(levelState == LevelState.Paused);
        }

        private void OnDestroy() {
            if (levelStateSource != null) {
                levelStateSource.StateChanged -= StateChanged;
            }
        }
    }

}



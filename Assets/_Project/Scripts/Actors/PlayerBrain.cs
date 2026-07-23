using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class PlayerBrain : MonoBehaviour {
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private PlayerLookController playerLookController;
        [SerializeField] private PlayerInteraction playerInteraction;
        [SerializeField] private WeaponOwner weaponOwner;
        ILevelStateSource levelStateSource;

        public void Initialize(ILevelStateSource levelStateSource, ILevelController levelController) {
            this.levelStateSource = levelStateSource;
            this.levelStateSource.StateChanged += OnStateChanged;
            canTick = levelStateSource.CurrentState == LevelState.Playing;
        }

        private bool canTick = false;
        private void OnStateChanged(LevelState levelState) {
            bool isPlaying = levelState == LevelState.Playing;

            playerMover.SetSimulationEnabled(isPlaying);
            canTick = isPlaying;
        }

        private void OnDestroy() {
            if (levelStateSource != null) {
                levelStateSource.StateChanged -= OnStateChanged;
            }
        }

        private void Update() {
            if (this.levelStateSource == null) {
                Debug.LogError("Level State Source is not initialized in PlayerBrain.");
                return;
            }
            if (!canTick) {
                return;
            }
            playerLookController.Tick();
            playerInteraction.Tick();
            weaponOwner.Tick(Time.deltaTime);
        }
    }
}

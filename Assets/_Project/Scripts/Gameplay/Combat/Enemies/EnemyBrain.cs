using _Project.Scripts.Combat.HSM;
using _Project.Scripts.Combat.Weapon;
using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay.Enums;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Combat.Enemies {
    public class EnemyBrain : MonoBehaviour {
        [SerializeField] private EnemyWeaponController weaponController;
        [SerializeField] private EnemyStateDriver stateDriver;
        private bool isPaused = false;
        private ILevelStateSource levelStateSource;
        public void Initalize(ILevelStateSource levelStateSource) {
            this.levelStateSource = levelStateSource;
            this.levelStateSource.StateChanged += OnStateChanged;
        }
        private void OnStateChanged(LevelState levelState) {
            if(levelState == LevelState.Completed) {
                isPaused = true;
                stateDriver.SetPaused(true);
            }
            if (levelState == LevelState.Paused) {
                stateDriver.SetPaused(true);
                isPaused = true;
            }
            else if (levelState == LevelState.Playing) {
                stateDriver.SetPaused(false);
                isPaused = false;
            }
        }

        private void OnDisable() {
            this.levelStateSource.StateChanged -= OnStateChanged;
        }

        void Update() {
            if (isPaused) {
                return;
            }
            stateDriver.Tick();
            weaponController.Tick();
        }

    }
}

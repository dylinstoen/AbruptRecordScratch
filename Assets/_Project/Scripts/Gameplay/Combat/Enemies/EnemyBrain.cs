using _Project.Scripts.Combat.HSM;
using _Project.Scripts.Combat.Weapon;
using _Project.Scripts.Core.Level.Interface;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Combat.Enemies {
    public class EnemyBrain : MonoBehaviour {
        [SerializeField] private EnemyWeaponController weaponController;
        [SerializeField] private EnemyStateDriver stateDriver;


        private ILevelStateSource levelStateSource;
        public void Initalize(ILevelStateSource levelStateSource) {
            this.levelStateSource = levelStateSource;
        }

        void Update() {
            if (levelStateSource == null) {
                Debug.LogError("LevelStateSource is null. Make sure to call Initialize() before Update().");
            }
            if (!levelStateSource.IsGameplayActive) {
                stateDriver.SetPaused(true);
                return;
            }

            if (stateDriver == null) {
                Debug.LogError("StateDriver is null. Make sure to assign it in the inspector.");
                return;
            }
            if (weaponController == null) {
                Debug.LogError("WeaponController is null. Make sure to assign it in the inspector.");
                return;
            }
            if(stateDriver.IsPaused) {
                stateDriver.SetPaused(false);
            }
            stateDriver.Tick();
            weaponController.Tick();
        }

    }
}

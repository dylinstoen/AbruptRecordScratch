using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay.Combat.Enemies;
using UnityEngine;


namespace _Project.Scripts.Gameplay {
    public class EnemySpawnService : MonoBehaviour {
        public void InitializeEnemiesInScene(ILevelStateSource levelStateSourceRef) {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var enemy in enemies) {
                EnemyBrain enemyBrain = enemy.GetComponent<EnemyBrain>();
                if (enemyBrain == null) {
                    Debug.LogError("Enemy missing enemy brain");
                    continue;
                }
                enemyBrain.Initalize(levelStateSourceRef);
            }
        }
        public void SpawnEnemies(ILevelStateSource levelStateSourceRef) {
            InitializeEnemiesInScene(levelStateSourceRef);
        }
    }
}

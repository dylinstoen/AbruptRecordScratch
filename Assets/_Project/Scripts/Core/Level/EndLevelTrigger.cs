using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Core.Level {
    public class EndLevelTrigger : MonoBehaviour {
        ILevelController levelController;

        public void Initialize(ILevelController levelController) {
            this.levelController = levelController;
        }
        // TODO: End level ALWAYS displays score -> goes to loading screen -> loading screen loads next level -> next level starts -> score is reset to 0. 
        private void OnTriggerEnter(Collider other) {
            if (this.levelController == null) {
                Debug.LogError("LevelController is not set on EndLevelTrigger. Please call Initialize() with a valid LevelController.");
                return;
            }
            if (!other.CompareTag("Player")) {
                return;
            }
            Debug.Log("Level completed!");
            levelController.CompleteLevel(); // Pass the score as a parameter
        }
    }
}



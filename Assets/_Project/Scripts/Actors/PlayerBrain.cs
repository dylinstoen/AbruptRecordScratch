using _Project.Scripts.Core.Level.Interface;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class PlayerBrain : MonoBehaviour {
        ILevelStateSource levelStateSource;
        public void Initialize(ILevelStateSource levelStateSource) {
            this.levelStateSource = levelStateSource;
        }

        private void Update() {
            
        }
    }
}

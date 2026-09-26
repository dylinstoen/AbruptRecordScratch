using UnityEngine;

namespace _Project.Scripts.GameRoot {
    public sealed class GameRoot : MonoBehaviour {
        public static GameRoot Instance { get; private set; }
        [field: SerializeField]
        public SettingsService Settings { get; private set;  }
        [field: SerializeField]
        public ProgressService Progress { get; private set; }
        private void Awake() {
            if (Instance != null && Instance != this) {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Settings.Initialize();
            Progress.Initialize();
        }
    }

}

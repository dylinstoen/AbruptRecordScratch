using _Project.Scripts.GameRoot;
using System.IO;
using UnityEngine;

namespace _Project.Scripts.GameRoot {
    public sealed class ProgressService : MonoBehaviour, IProgressService {
        private ProgressData _data;

        private string FilePath => Path.Combine(Application.persistentDataPath, "progress.json");

        public bool HasProgress => _data.HighestCompletedLevel > 0;

        public int HighestCompletedLevel => _data.HighestCompletedLevel;

        public int CurrentLevel => _data.HighestCompletedLevel + 1;

        public void Initialize() {
            Load();
        }

        public void CompleteLevel(int level) {
            if (level <= _data.HighestCompletedLevel)
                return;

            _data.HighestCompletedLevel = level;

            Save();
        }

        public bool IsLevelCompleted(int level) {
            return level <= _data.HighestCompletedLevel;
        }

        public bool IsLevelUnlocked(int level) {
            return level <= CurrentLevel;
        }

        public void ResetProgress() {
            _data = new ProgressData();

            Save();
        }

        public void Save() {
            string json = JsonUtility.ToJson(_data, true);

            File.WriteAllText(FilePath, json);
        }

        private void Load() {
            if (!File.Exists(FilePath)) {
                _data = new ProgressData();
                Save();
                return;
            }

            string json = File.ReadAllText(FilePath);

            ProgressData loaded = JsonUtility.FromJson<ProgressData>(json);

            _data = loaded ?? new ProgressData();
        }
    }
}

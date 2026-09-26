using UnityEngine;

namespace _Project.Scripts.GameRoot {
    public interface IProgressService {
        bool HasProgress { get; }

        int HighestCompletedLevel { get; }
        int CurrentLevel { get; }

        void CompleteLevel(int level);

        bool IsLevelCompleted(int level);
        bool IsLevelUnlocked(int level);

        void ResetProgress();
        void Save();
    }
}


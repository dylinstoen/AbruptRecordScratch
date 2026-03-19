using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    [CreateAssetMenu(menuName = "Impact/TargetAudioImpactProfile", fileName = "TargetAudioImpactProfile")]
    public class TargetAudioImpactProfileSO : ScriptableObject {
        public AudioCue audioCue;
        public BlendType blendType;
    }
}
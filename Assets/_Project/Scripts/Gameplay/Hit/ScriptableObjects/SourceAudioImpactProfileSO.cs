using _Project.Scripts.Audio.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    [CreateAssetMenu(menuName = "Impact/SourceAudioImpactEffect", fileName = "SourceAudioImpactEffect")]
    public class SourceAudioImpactProfileSO: ScriptableObject {
        public AudioCue audioCue;
    }
}
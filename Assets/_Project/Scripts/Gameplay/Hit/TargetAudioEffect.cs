using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class TargetAudioEffect : MonoBehaviour, ITargetAudioEffect {
        [SerializeField] private TargetAudioImpactProfileSO targetAudioProfile;
        public TargetAudioImpactProfileSO TargetAudioProfile => targetAudioProfile;
    }
}
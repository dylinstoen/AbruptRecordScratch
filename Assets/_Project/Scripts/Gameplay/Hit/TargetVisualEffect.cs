using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class TargetVisualEffect : MonoBehaviour, ITargetVisualEffect {
        [SerializeField] private TargetVisualImpactProfileSO targetVisualProfile;
        public TargetVisualImpactProfileSO TargetVisualProfile => targetVisualProfile;
    }
}
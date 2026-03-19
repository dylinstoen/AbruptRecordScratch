using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    [CreateAssetMenu(menuName = "Impact/TargetVisualImpactProfile", fileName = "TargetVisualImpactProfile")]
    public class TargetVisualImpactProfileSO : ScriptableObject {
        public SplatterType SplatterType;
        public BlendType SplatterBlendType;
        public StainType StainType;
        public BlendType StainBlendType;
    }
}
using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    [CreateAssetMenu(menuName = "Impact/TargetImpactProfile", fileName = "TargetImpactProfile")]
    public class TargetImpactProfileSO : ScriptableObject {
        public SplatterType splatterType;
        public BlendType splatterBlendType;
        public StainType stainType;
        public BlendType stainBlendType;
    }
}
using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    [CreateAssetMenu(menuName = "Impact/SourceVisualImpactEffect", fileName = "SourceVisualImpactEffect")]
    public class SourceVisualImpactProfileSO : ScriptableObject {
        public SplatterType SplatterType;
        public StainType StainType;
    }
}
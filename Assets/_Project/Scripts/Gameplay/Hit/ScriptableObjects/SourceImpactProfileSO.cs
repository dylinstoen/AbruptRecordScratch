using _Project.Scripts.Gameplay.Enums;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    [CreateAssetMenu(menuName = "Impact/SourceImpactEffect", fileName = "SourceImpactEffect")]
    public class SourceImpactProfileSO : ScriptableObject {
        public SplatterType SplatterType;
        public StainType StainType;
    }
}
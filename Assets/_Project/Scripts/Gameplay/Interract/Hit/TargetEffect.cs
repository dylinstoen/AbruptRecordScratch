using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class TargetEffect : MonoBehaviour, ITargetEffect {
        [SerializeField] private TargetImpactProfileSO targetProfile;
        public TargetImpactProfileSO TargetProfile => targetProfile;
    }
}
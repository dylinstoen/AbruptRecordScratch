using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Combat.Weapon {
    public abstract class EnemyEmitterMode {
        private float _damage;
        public readonly float Range;
        public readonly IImpactService Impact;
        public readonly SourceVisualImpactProfileSO SourceVisualImpactProfile;
        public readonly SourceAudioImpactProfileSO SourceAudioImpactProfile;
        protected EnemyEmitterMode(float range, float damage, IImpactService impactService, SourceVisualImpactProfileSO sourceVisualImpactProfile, SourceAudioImpactProfileSO sourceAudioImpactProfile) {
            _damage = damage;
            Range = range;
            Impact = impactService;
            SourceVisualImpactProfile = sourceVisualImpactProfile;
            SourceAudioImpactProfile = sourceAudioImpactProfile;
        }

        public abstract void Emit(Vector3 position, Vector3 direction);
    }
}
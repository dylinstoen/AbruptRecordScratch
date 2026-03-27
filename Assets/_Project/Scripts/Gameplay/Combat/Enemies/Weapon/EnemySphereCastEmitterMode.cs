using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Combat.Weapon {
    public class EnemySphereCastEmitterMode : EnemyEmitterMode {
        public float Size;
        public EnemySphereCastEmitterMode(float range, float damage, float size, IImpactService impactService, SourceVisualImpactProfileSO sourceVisualImpactProfile, SourceAudioImpactProfileSO sourceAudioImpactProfile) : base(range, damage, impactService, sourceVisualImpactProfile, sourceAudioImpactProfile) {
            Size = size;
        }

        public override void Emit(Vector3 position, Vector3 direction) {
            
        }
    }
}
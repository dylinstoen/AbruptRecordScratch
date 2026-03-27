using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Combat.Weapon {
    public class EnemyRaycastEmitterMode : EnemyEmitterMode {
        public EnemyRaycastEmitterMode(float range, float damage, IImpactService impactService,
            SourceVisualImpactProfileSO sourceVisualImpactProfile, 
            SourceAudioImpactProfileSO sourceAudioImpactProfile) : 
            base(range, damage, impactService,  sourceVisualImpactProfile, sourceAudioImpactProfile) { }

        public override void Emit(Vector3 position, Vector3 direction) { }
    }
}
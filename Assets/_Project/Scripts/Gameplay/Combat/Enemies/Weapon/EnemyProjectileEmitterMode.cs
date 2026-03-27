using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Combat.Weapon {
    public class EnemyProjectileEmitterMode : EnemyEmitterMode {
        protected ProjectileInstance ProjectileInstance;
        public EnemyProjectileEmitterMode(float range, float damage, IImpactService impactService, 
            ProjectileInstance projectileInstance,  SourceVisualImpactProfileSO sourceVisualImpactProfile, 
            SourceAudioImpactProfileSO sourceAudioImpactProfile) : base(range, damage, impactService, sourceVisualImpactProfile, sourceAudioImpactProfile) {
            ProjectileInstance = projectileInstance;
        }

        public override void Emit(Vector3 position, Vector3 direction) {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            var inst = Object.Instantiate(ProjectileInstance, position, targetRotation);
            inst.Initialize(Impact, SourceVisualImpactProfile, SourceAudioImpactProfile, Range);
        }
    }
}
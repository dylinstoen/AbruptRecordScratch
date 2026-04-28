using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "SphereCast", menuName = "Weapon/Emitters/SphereCast")]
    public class SphereCastEmitterModeSO : EmitterModeSO {
        [SerializeField] private float radius;
        [SerializeField] private float maxDistance;
        public override IEmitterMode Create(IImpactService impactService, int damage, GameObject owner, SourceVisualImpactProfileSO sourceVisualImpactProfile, LayerMask hitLayerMask) {
            return new SphereCastEmitterMode(radius, maxDistance, damage, owner, impactService, sourceVisualImpactProfile, hitLayerMask);
        }
    }
}
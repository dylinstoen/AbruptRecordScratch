using _Project.Scripts.Combat;
using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "Raycast", menuName = "Weapon/Emitters/Raycast")]
    public class RaycastEmitterModeSo : EmitterModeSO {
        [SerializeField] private float maxDistance;
        public override IEmitterMode Create(IImpactService impactService, int damage, GameObject owner, SourceImpactProfileSO sourceImpactProfile) {
            return new RaycastEmitterMode(maxDistance, damage, owner,  impactService, sourceImpactProfile);
        }
    }
}
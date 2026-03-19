using _Project.Scripts.Combat;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Combat.Struct;
using _Project.Scripts.Gameplay.Structs;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public class RaycastEmitterMode : IEmitterMode {
        private float _maxDistance;
        private IImpactService _impactService;
        private int _damage;
        private GameObject _owner;
        private SourceVisualImpactProfileSO _sourceVisualImpactProfile;
        public RaycastEmitterMode(float maxDistance, int damage, GameObject owner, IImpactService impactService, SourceVisualImpactProfileSO sourceVisualImpactProfile) {
            _maxDistance = maxDistance;
            _impactService = impactService;
            _damage = damage;
            _owner = owner;
            _sourceVisualImpactProfile = sourceVisualImpactProfile;
        }
        public void Fire(Ray ray) {
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, _maxDistance)) {
                HitContext hitctx = new HitContext(hit.point, hit.normal, hit.collider, _owner, _damage);
                _impactService.ProcessHitVisual(hitctx, _sourceVisualImpactProfile);
                _impactService.ProcessHitLogic(hitctx);
            }
        }
    }
}
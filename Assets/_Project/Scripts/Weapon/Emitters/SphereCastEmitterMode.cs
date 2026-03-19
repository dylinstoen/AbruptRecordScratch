using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public class SphereCastEmitterMode : IEmitterMode {
        private float _radius;
        private float _maxDistance;
        private IImpactService _impactService;
        private int _damage;
        private GameObject _owner;
        private SourceVisualImpactProfileSO _sourceVisualImpactProfile;
        public SphereCastEmitterMode(float radius, float maxDistance, int damage, GameObject owner, IImpactService impactService, SourceVisualImpactProfileSO sourceVisualImpactProfile) {
            _radius = radius;
            _impactService = impactService;
            _damage = damage;
            _owner = owner;
            _maxDistance = maxDistance;
            _sourceVisualImpactProfile = sourceVisualImpactProfile;
        }

  
        public void Fire(Ray ray) {
            if (Physics.SphereCast(ray.origin, _radius, ray.direction, out RaycastHit hit, _maxDistance)) {
                HitContext hitctx = new HitContext(hit.point, hit.normal, hit.collider, _owner, _damage);
                _impactService.ProcessHitVisual(hitctx, _sourceVisualImpactProfile);
                _impactService.ProcessHitLogic(hitctx);
            }
        }
        
    }
}
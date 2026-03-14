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
        private SourceImpactProfileSO _sourceImpactProfile;
        public RaycastEmitterMode(float maxDistance, int damage, GameObject owner, IImpactService impactService, SourceImpactProfileSO sourceImpactProfile) {
            _maxDistance = maxDistance;
            _impactService = impactService;
            _damage = damage;
            _owner = owner;
            _sourceImpactProfile = sourceImpactProfile;
        }
        public void Fire(Ray ray) {
            if (Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit, _maxDistance)) {
                // TODO: Communicate hit with other systems
                HitContext hitctx = new HitContext(hit.point, hit.normal, hit.collider, _owner, _damage);
                _impactService.ProcessHit(hitctx, _sourceImpactProfile);
            }
        }
    }
}
using _Project.Scripts.Combat;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Combat.Struct;
using _Project.Scripts.Gameplay.Structs;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public class RaycastEmitterMode : IEmitterMode {
        private float _maxDistance;
        private IHitService _hitService;
        private int _damage;
        private GameObject _owner;
        private SourceImpactProfileSO _sourceImpactProfile;
        public RaycastEmitterMode(float maxDistance, int damage, GameObject owner, IHitService hitService, SourceImpactProfileSO sourceImpactProfile) {
            _maxDistance = maxDistance;
            _hitService = hitService;
            _damage = damage;
            _owner = owner;
            _sourceImpactProfile = sourceImpactProfile;
        }
        public void Fire(WeaponUseContext ctx) {
            if (Physics.Raycast(ctx.AimRay.origin, ctx.AimRay.direction, out RaycastHit hit, _maxDistance)) {
                // TODO: Communicate hit with other systems
                HitContext hitctx = new HitContext(hit.point, hit.normal, hit.collider, _owner, _damage);
                _hitService.ProcessHit(hitctx, _sourceImpactProfile);
            }
        }
    }
}
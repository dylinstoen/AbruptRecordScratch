using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public class RaycastEmitterMode : IEmitterMode {
        private float _maxDistance;
        public RaycastEmitterMode(float maxDistance) {
            _maxDistance = maxDistance;
        }
        public void Fire(WeaponUseContext ctx) {
            if (Physics.Raycast(ctx.AimRay.origin, ctx.AimRay.direction, out RaycastHit hit, _maxDistance)) {
                Debug.Log(hit.transform.name);
                // TODO: Communicate hit with other systems
            }
            else {
                Debug.LogWarning("Raycast miss");
            }
        }
    }
}
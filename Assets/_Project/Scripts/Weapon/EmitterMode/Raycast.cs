using FPS.Weapon.Stucts;
using UnityEngine;

namespace FPS.Weapon {
    public class Raycast : EmitterMode {
        private int bulletsPerShot;
        private Vector3 positionToFire;
        private float range;
        private LayerMask hitLayerMask;

        public Raycast(Weapon weapon, int bulletsPerShot, float range, Vector3 positionToFire, LayerMask hitLayerMask) : base(weapon) {
            this.bulletsPerShot = bulletsPerShot;
            this.positionToFire = positionToFire;
            this.range = range;
            this.hitLayerMask = hitLayerMask;
        }
        public override void Fire(Vector3 directionToFire) {
            for (int i = 0; i < bulletsPerShot; i++) {
                if (Physics.Raycast(positionToFire, directionToFire, out RaycastHit hit, range, hitLayerMask)) {
                    HitResult result = new HitResult();
                    result.Collider = hit.collider;
                    result.Point = hit.point;
                    result.Normal = hit.normal;
                    weapon.Hit(result);
                }
            }
        }
    }
}


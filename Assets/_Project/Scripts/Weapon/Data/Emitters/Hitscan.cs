using UnityEngine;

namespace FPS.Weapon {
    [CreateAssetMenu(fileName = "Hitscan", menuName = "Weapon/Emitter/Hitscan")]
    public class Hitscan : Emitter {
        public LayerMask hitMask;
        [SerializeField] private bool debug = false;
        public override HitResult Fire(Vector3 firePoint, Vector3 dir, float distance) {
            HitResult result = new HitResult();
            if (Physics.Raycast(firePoint, dir, out RaycastHit hit, distance, hitMask)) {
                result.Hit = true;
                result.Point = hit.point;
                result.Normal = hit.normal;
                result.Collider = hit.collider;
            }
            if (debug) {
                Debug.DrawRay(firePoint, dir * distance, Color.red);
            }
            return result;
        }
    }
}


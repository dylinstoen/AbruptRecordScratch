using UnityEngine;

namespace FPS.Weapon {
    public struct HitResult
    {
        public bool Hit;
        public Vector3 Point;
        public Vector3 Normal;
        public Collider Collider;
    }
}

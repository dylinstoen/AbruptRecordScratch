using FPS.Aiming;
using FPS.Input;
using Unity.VisualScripting;
using UnityEngine;

namespace FPS.Weapon {
    public struct HitResult {
        public bool Hit;
        public Vector3 Point;
        public Vector3 Normal;
        public Collider Collider;
    }

    public struct WeaponSnapshot {
        public Vector3 aimOrigin { get; }
        public bool primaryFireState { get; }
        public float DeltaTime { get; }

        public WeaponSnapshot(Vector3 aimOrigin, bool primaryFireState, float DeltaTime) {
            this.aimOrigin = aimOrigin;
            this.primaryFireState = primaryFireState;
            this.DeltaTime = DeltaTime;
        }
    }
}

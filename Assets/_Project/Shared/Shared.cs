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

    public struct WeaponViewSnapshot {
        public Vector3 aimSourceForward { get; }
        public Vector3 aimSourcePosition { get; }
        public WeaponViewSnapshot(Vector3 aimSourceForward, Vector3 aimSourcePosition) {
            this.aimSourceForward = aimSourceForward;
            this.aimSourcePosition = aimSourcePosition;
        }
    }
    public struct WeaponControllerSnapshot {

        public bool primaryFireState { get; }
        public Vector3 directionToFire { get; }
        public float DeltaTime { get; }

        public WeaponControllerSnapshot(Vector3 directionToFire, bool primaryFireState, float DeltaTime) {
            this.directionToFire = directionToFire;
            this.primaryFireState = primaryFireState;
            this.DeltaTime = DeltaTime;
        }
    }
}

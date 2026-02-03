using FPS.Aiming;
using FPS.Input;
using UnityEngine;

namespace FPS.Weapon {
    public struct HitResult {
        public bool Hit;
        public Vector3 Point;
        public Vector3 Normal;
        public Collider Collider;
    }

    public struct WeaponContext {
        public IAimSource aimSource { get; }
        public IFireInput fireInput { get; }

        public WeaponContext(IAimSource aimSource, IFireInput fireInput) {
            this.aimSource = aimSource;
            this.fireInput = fireInput;
        }
        public WeaponContext(IFireInput fireInput, IAimSource aimSource) {
            this.aimSource = aimSource;
            this.fireInput = fireInput;
        }
    }
}

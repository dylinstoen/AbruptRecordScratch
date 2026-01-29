using UnityEngine;

namespace FPS.Weapon {
    public abstract class Emitter : ScriptableObject {
        public abstract HitResult Fire(Vector3 firePoint, Vector3 dir, float distance);
    }

}

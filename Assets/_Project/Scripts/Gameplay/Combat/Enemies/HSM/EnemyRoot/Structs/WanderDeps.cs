using UnityEngine;

namespace _Project.Scripts.Combat.HSM.Structs {
    [System.Serializable]
    public struct WanderDeps {
        public float wanderRadius;
        public Vector3 startPosition;
        public Collider wanderZone;
    }
}
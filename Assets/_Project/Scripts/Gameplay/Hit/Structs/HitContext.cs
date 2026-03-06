using _Project.Scripts.Actors;
using UnityEngine;

namespace _Project.Scripts.Gameplay.Structs {
    public struct HitContext {
        public readonly Vector3 Position;
        public readonly Vector3 Normal;
        public readonly Collider HitCollider;
        public readonly GameObject Instigator;
        public readonly int Damage;
        public readonly IDamageable Damageable;
        public bool IsDamageable => Damageable != null;

        public HitContext(Vector3 position, Vector3 normal, Collider hitCollider, GameObject instigator, int damage) {
            Position = position;
            Normal = normal;
            HitCollider = hitCollider;
            Instigator = instigator;
            Damage = damage;
            Damageable = null;
        }
        public HitContext(Vector3 position, Vector3 normal, Collider hitCollider, GameObject instigator, int damage, IDamageable damageable) {
            Position = position;
            Normal = normal;
            HitCollider = hitCollider;
            Instigator = instigator;
            Damage = damage;
            Damageable = damageable;
        }
    }
}
using UnityEngine;

namespace _Project.Scripts.Gameplay.Combat.Struct {
    public struct DamageContext {
        public readonly int Amount;
        public readonly Vector3 HitPoint;
        public readonly Vector3 HitNormal;
        public readonly GameObject Instigator;

        public DamageContext(int amount, Vector3 hitPoint, Vector3 hitNormal, GameObject instigator) {
            Amount = amount;
            HitPoint = hitPoint;
            HitNormal = hitNormal;
            Instigator = instigator;
        }
    }
}
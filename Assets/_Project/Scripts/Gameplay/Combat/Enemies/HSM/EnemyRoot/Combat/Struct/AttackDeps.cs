using UnityEngine;

namespace _Project.Scripts.Combat.HSM {
    [System.Serializable]
    public struct AttackDeps {
        public int shotCount;
        public float attackCoolDown;
        public float rotationSpeed;
        public float maxAttackAngle;
    }
}
using _Project.Scripts.Combat.Weapon;
using UnityEngine;

namespace _Project.Scripts.Combat.HSM {
    [System.Serializable]
    public struct AttackDeps {
        public int shotCount;
        public float rotationSpeed;
        public float maxAttackAngle;
        public EnemyWeaponController enemyWeaponController;
    }
}
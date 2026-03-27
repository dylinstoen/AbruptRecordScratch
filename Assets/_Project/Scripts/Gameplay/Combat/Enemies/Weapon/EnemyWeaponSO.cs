using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Combat.Weapon {
    [CreateAssetMenu(fileName = "WeaponSO", menuName = "Enemy/Weapon")]
    public class EnemyWeaponSO : ScriptableObject {
        public enum EnemyFireMode {SingleShot, FullAuto}
        public enum EnemyEmitterMode {Raycast, SphereCast, Projectile}
        public SourceVisualImpactProfileSO sourceVisualImpactProfile;
        public SourceAudioImpactProfileSO sourceAudioImpactProfile;
        [Tooltip("Only for projectile, projectile prefab to spawn")]
        public ProjectileInstance projectileInstance;
        public int damage = 0;
        [Tooltip("For raycast and projectile its distance traveled and for spherecast its center point")]
        public int range = 0;
        [Tooltip("Time in between attacks")]
        public float coolDown = 0;
        [Tooltip("Only for spherecast, size of sphere")]
        public float sphereSize = 0;
        [Tooltip("Time until the actual hit is fired (Make sure its less than or equal to attack time)")]
        public float emissionDelay = 0;
        [Tooltip("Amount of time the attack actually takes (total time between attacks is attackTime + coolDown)")]
        public float attackTime = 0;
        public EnemyFireMode enemyFireMode;
        public EnemyEmitterMode enemyEmitterMode;
        
    }
}
using System;
using _Project.Scripts.Gameplay;

namespace _Project.Scripts.Combat.Weapon {
    public static class EnemyWeaponFactory {
        public static EnemyFireMode Create(EnemyWeaponSO enemyWeapon, IImpactService impactService) {
            EnemyEmitterMode emitterMode = enemyWeapon.enemyEmitterMode switch {
                EnemyWeaponSO.EnemyEmitterMode.Raycast => new EnemyRaycastEmitterMode(enemyWeapon.range, enemyWeapon.damage, impactService, enemyWeapon.sourceVisualImpactProfile, enemyWeapon.sourceAudioImpactProfile),
                EnemyWeaponSO.EnemyEmitterMode.Projectile => new EnemyProjectileEmitterMode(enemyWeapon.range, enemyWeapon.damage, impactService, enemyWeapon.projectileInstance, enemyWeapon.sourceVisualImpactProfile, enemyWeapon.sourceAudioImpactProfile),
                EnemyWeaponSO.EnemyEmitterMode.SphereCast => new EnemySphereCastEmitterMode(enemyWeapon.range, enemyWeapon.damage, enemyWeapon.sphereSize, impactService, enemyWeapon.sourceVisualImpactProfile, enemyWeapon.sourceAudioImpactProfile),
                _ => throw new ArgumentOutOfRangeException()
            };

            return enemyWeapon.enemyFireMode switch {
                EnemyWeaponSO.EnemyFireMode.SingleShot => new EnemySingleShotFireMode(emitterMode, enemyWeapon.coolDown),
                EnemyWeaponSO.EnemyFireMode.FullAuto => new EnemyFullAutoFireMode(emitterMode, enemyWeapon.coolDown),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}
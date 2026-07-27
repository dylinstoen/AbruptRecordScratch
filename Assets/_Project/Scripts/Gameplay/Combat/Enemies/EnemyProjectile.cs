using _Project.Scripts.Combat.Weapon;
using _Project.Scripts.Core;
using _Project.Scripts.Core.Level.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting;
using UnityEngine;
using _Project.Scripts.Gameplay.Enums;

namespace Assets._Project.Scripts.Gameplay.Combat.Enemies
{
    [System.Serializable]
    public struct Projectile {
        public GameObject projectilePrefab;
        public Transform firePoint;
    }

    public class EnemyProjectile : MonoBehaviour {
        [SerializeField] private Projectile[] projectiles;

        private ILevelStateSource levelStateSource;
        public void Inititalize(ILevelStateSource levelStateSource) {
            this.levelStateSource = levelStateSource;
        }
        public void Fire() {
            if (levelStateSource == null || levelStateSource.CurrentState != LevelState.Playing) {
                return;
            }
            foreach (Projectile projectile in projectiles) {
                if (projectile.projectilePrefab == null || projectile.firePoint == null)
                    continue;

                Vector3 dir = projectile.firePoint.forward;

                var inst = Instantiate(projectile.projectilePrefab, projectile.firePoint.position, Quaternion.LookRotation(dir, Vector3.up));
                ProjectileInstance projInstance = inst.GetComponent<ProjectileInstance>();
                if (projInstance != null) {
                    projInstance.Initialize(this.levelStateSource, SceneServiceLocator.Current.Impact, 100f);
                }
            }
            
        }
        private void OnDrawGizmos() {
            if(projectiles == null || projectiles.Length <= 0) {
                return;
            }
            foreach (Projectile projectile in projectiles) {
                if (projectile.firePoint == null)
                    continue;

                Gizmos.DrawLine(projectile.firePoint.position, projectile.firePoint.position + projectile.firePoint.forward.normalized * 2f);
            }
        }
    }
}

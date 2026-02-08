using UnityEngine;
using FPS.Weapon.Stucts;
namespace FPS.Weapon {
    public class Weapon : MonoBehaviour {
        [SerializeField] private DataSO data;
        [SerializeField] private View view;
        [SerializeField] private Transform firePoint;
        private Vector3 directionToFire;
        private FireMode fireMode;
        private EmitterMode emitterMode;
        private float currentAmmo = 0;
        private void Start() {
            currentAmmo = data.Ammo;
            switch (data.FireMode) {
                case Enums.FireMode.SingleShot:
                    fireMode = new SingleShot(this, data.coolDown);
                    break;
                case Enums.FireMode.Auto:
                    fireMode = new Auto(this, data.coolDown);
                    break;
                case Enums.FireMode.Burst:
                    // TODO: fireMode = new Burst(this)
                    break;
            }

            switch (data.emitterMode) {
                case Enums.EmitterMode.Projectile:
                    emitterMode = new Raycast(this, data.bulletsPerShot, data.Range, firePoint.position, data.hitLayerMask);
                    break;
                case Enums.EmitterMode.Raycast:
                    break;
            }
        }
        public void Tick(bool inputState, Vector3 directionToFire) {
            this.directionToFire = directionToFire;
            if (fireMode == null) return;
            fireMode.Tick(inputState, Time.deltaTime);
        }

        public void LateTick(Vector3 aimSourceForward, Vector3 aimSourcePosition) {
            view.LateTick(aimSourceForward, aimSourcePosition);
        }

        public void TryFire() {
            if (emitterMode == null) return;
            if (currentAmmo <= 0) return;
            currentAmmo--;
            emitterMode.Fire(directionToFire);
        }

        public void Hit(HitResult result) {
            Debug.Log("Hit " + result.Point);
        }
    }
}


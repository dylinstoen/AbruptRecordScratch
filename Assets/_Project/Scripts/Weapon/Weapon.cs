using UnityEngine;
using FPS.Weapon.Stucts;
namespace FPS.Weapon {
    public class Weapon : MonoBehaviour {
        [SerializeField] private DataSO data;
        [SerializeField] private View view;
        [SerializeField] private Transform firePoint;
        private FireMode fireMode;
        private float currentAmmo = 0;
        private void Start() {
            currentAmmo = data.Ammo;
            switch (data.FireMode) {
                case Enums.FireMode.SingleShot:
                    fireMode = new SingleShot(this);
                    break;
                case Enums.FireMode.Auto:
                    // TODO: fireMode = new Auto(this)
                    break;
                case Enums.FireMode.Burst:
                    // TODO: fireMode = new Burst(this)
                    break;
            }

            switch (data.emitterMode) {
                case Enums.EmitterMode.Projectile:
                    break;
                case Enums.EmitterMode.Raycast:
                    break;
            }
        }
        public void Tick(bool inputState, Vector3 directionToFire) {
            fireMode.Tick(inputState, Time.deltaTime);
        }

        public void LateTick(Vector3 aimSourceForward, Vector3 aimSourcePosition) {
            view.LateTick(aimSourceForward, aimSourcePosition);
        }

        public void TryFire() {
            if (currentAmmo <= 0) return;
            currentAmmo--;
            // Emit should control how many shots are EMITTED per fire
            // Emitter.Emit(directionToFire);
        }
    }
}


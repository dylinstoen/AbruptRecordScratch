using System;
using FPS.Aiming;
using FPS.Input;
using UnityEngine;

namespace FPS.Weapon {
    public class Controller : MonoBehaviour {
        // Aim Source, Input, and firePoint, is a scene reference which means it gets injected by objects in the scene
        [SerializeField] private Data data;
        public Transform firePoint;
        public View view;
        private float currentAmmo = 0;
        private void Start() {
            currentAmmo = data.Ammo;
        }
        public void Tick(in WeaponControllerSnapshot controllerSnapshot) {
            if (currentAmmo <= 0 || !data.mode.CanFire(controllerSnapshot.primaryFireState, controllerSnapshot.DeltaTime)) {
                return;
            }
            Vector3 dir = controllerSnapshot.directionToFire;
            HitResult res = data.emitter.Fire(firePoint.position, dir, data.Range);
            Debug.Log("Hit " + res.Hit);
            view.OnFired();
        }

        public void LateTick(in WeaponViewSnapshot viewSnapshot) {
            view.UpdateView(viewSnapshot.aimSourceForward, viewSnapshot.aimSourcePosition);
        }

    }
}

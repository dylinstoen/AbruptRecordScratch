using System;
using FPS.Aiming;
using FPS.Input;
using UnityEngine;

namespace FPS.Weapon {
    public class Controller : MonoBehaviour {
        // Aim Source, Input, and firePoint, is a scene reference which means it gets injected by objects in the scene
        [SerializeField] private Data data;
        public IAimSource aim;
        public Transform firePoint;
        public View view;
        private float currentAmmo = 0;

        public void Inject(IAimSource aim) {
            this.aim = aim;
        }
        private void Start() {
            currentAmmo = data.Ammo;
        }
        public void Tick(in WeaponSnapshot snapshot) {
            if (currentAmmo <= 0 || !data.mode.CanFire(snapshot.primaryFireState, snapshot.DeltaTime)) {
                return;
            }
            Vector3 dir = aim.Forward;
            HitResult res = data.emitter.Fire(firePoint.position, dir, data.Range);
            Debug.Log("Hit " + res.Hit);
            view.OnFired();
        }
    }
}

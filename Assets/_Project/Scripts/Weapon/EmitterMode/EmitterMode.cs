using UnityEngine;

namespace FPS.Weapon {
    public abstract class EmitterMode {
        protected Weapon weapon;
        protected EmitterMode(Weapon weapon) {
            this.weapon = weapon;
        }
        public abstract void Fire(Vector3 directionToFire);
    }
}


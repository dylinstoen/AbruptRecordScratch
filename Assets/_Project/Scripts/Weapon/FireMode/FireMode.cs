using UnityEngine;

namespace FPS.Weapon {
    public abstract class FireMode {
        protected Weapon weapon;
        protected FireMode(Weapon weapon) {
            this.weapon = weapon;
        }
        // Decide when should this weapon shoot
        public abstract void Tick(bool inputState, float deltaTime);
    }
}


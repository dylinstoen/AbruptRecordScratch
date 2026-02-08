using UnityEngine;

namespace FPS.Weapon {
    public abstract class FireMode {
        protected Weapon weapon;
        protected float coolDown;
        protected FireMode(Weapon weapon, float coolDown) {
            this.weapon = weapon;
            this.coolDown = coolDown;
        }
        // Decide when should this weapon shoot
        public abstract void Tick(bool inputState, float deltaTime);
    }
}


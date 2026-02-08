using UnityEngine;

namespace FPS.Weapon {
    public class Auto : FireMode {
        private float coolDownRemaining;
        public Auto(Weapon weapon, float coolDown) : base(weapon, coolDown) { }

        public override void Tick(bool inputState, float deltaTime) {
            if (coolDownRemaining > 0f) {
                coolDownRemaining -= deltaTime;
            }
            if (coolDownRemaining <= 0 && inputState) {
                Debug.Log("Firing");
                coolDownRemaining = coolDown;
                weapon.TryFire();
            }
        }
    }
}
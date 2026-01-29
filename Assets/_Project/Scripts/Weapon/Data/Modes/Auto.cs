using UnityEngine;

namespace FPS.Weapon {
    [CreateAssetMenu(fileName = "Auto", menuName = "Weapon/Mode/Auto")]
    public class Auto : Mode {
        [SerializeField] private float fireRate = 1.0f;
        private float coolDownTimer = 0.0f;
        public override bool CanFire(bool triggerState, float deltaTime) {
            coolDownTimer += deltaTime;
            if (!triggerState || coolDownTimer < 1f / fireRate)
                return false;
            coolDownTimer = 0.0f;
            return true;
        }
    } 
}
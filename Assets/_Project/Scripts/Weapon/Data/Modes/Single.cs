using UnityEngine;

namespace FPS.Weapon {
    [CreateAssetMenu(fileName = "Single", menuName = "Weapon/Mode/Single")]
    public class Single : Mode {
        [SerializeField] private float fireRate = 1.0f;
        private bool wasTriggerHeld = false;
        private float coolDownTimer = 0.0f;
        public override bool CanFire(bool triggerState, float deltaTime) {
            coolDownTimer += deltaTime;
            bool triggerPressedThisFrame = triggerState && !wasTriggerHeld;
            wasTriggerHeld = triggerState;
            if (!triggerPressedThisFrame)
                return false;
            if (coolDownTimer < 1f / fireRate)
                return false;
            coolDownTimer = 0.0f;
            return true;
        }
    } 
}


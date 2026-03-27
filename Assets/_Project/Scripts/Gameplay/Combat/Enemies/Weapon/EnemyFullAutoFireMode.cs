using UnityEngine;

namespace _Project.Scripts.Combat.Weapon {
    public class EnemyFullAutoFireMode : EnemyFireMode {
        private bool isFiring = false;
        public EnemyFullAutoFireMode(EnemyEmitterMode emitterMode, float coolDown) : base(emitterMode, coolDown) { }

        public override void OnStartFire(Vector3 position, Vector3 direction) {
            isFiring = true;
        }

        public override void OnStopFire() {
            isFiring = false;
        }

        public override void OnUpdate(Vector3 position, Vector3 direction, float deltaTime) {
            if (CoolDownTimer.IsRunning) {
                CoolDownTimer.Tick(deltaTime);
                return;
            }

            if (!isFiring) return;
            EmitterMode.Emit(position, direction);
            CoolDownTimer.Start();
        }
    }
}
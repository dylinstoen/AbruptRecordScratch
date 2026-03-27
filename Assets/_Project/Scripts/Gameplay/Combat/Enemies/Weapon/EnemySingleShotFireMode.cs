using UnityEngine;

namespace _Project.Scripts.Combat.Weapon {
    public class EnemySingleShotFireMode : EnemyFireMode {
        public EnemySingleShotFireMode(EnemyEmitterMode emitterMode, float coolDown) : base(emitterMode, coolDown) { }

        public override void OnStartFire(Vector3 position, Vector3 direction) {
            if (!CoolDownTimer.IsFinished)
                return;
            EmitterMode.Emit(position, direction);
            CoolDownTimer.Start();
        }

        public override void OnStopFire() {
        }

        public override void OnUpdate(Vector3 position, Vector3 direction, float deltaTime) {
            if (CoolDownTimer.IsRunning) {
                CoolDownTimer.Tick(deltaTime);
            }
        }
    }
}
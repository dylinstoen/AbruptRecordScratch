using _Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Combat.Weapon {
    public abstract class EnemyFireMode {
        protected EnemyEmitterMode EmitterMode;
        protected CountdownTimer CoolDownTimer;
        protected EnemyFireMode(EnemyEmitterMode emitterMode, float coolDown) {
            EmitterMode = emitterMode;
            CoolDownTimer = new CountdownTimer(coolDown);
        }
        public abstract void OnStartFire(Vector3 position, Vector3 direction);
        public abstract void OnStopFire();
        public abstract void OnUpdate(Vector3 position, Vector3 direction, float deltaTime);

        public bool CanFire() => CoolDownTimer.IsFinished;
    }
}
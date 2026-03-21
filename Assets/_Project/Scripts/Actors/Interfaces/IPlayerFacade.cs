using UnityEngine;

namespace _Project.Scripts.Actors {
    public interface IPlayerFacade {
        public Transform AimPoint { get; }
        public Transform Root { get; }
        public IHealthEvents HealthEvents { get; }
        public IDeathEvents DeathEvents { get; }
        public IAmmoEvents AmmoEvents { get; }
        public void BindServices(PlayerDeps deps);
    }
}
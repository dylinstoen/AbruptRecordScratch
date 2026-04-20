using UnityEngine;

namespace _Project.Scripts.Actors {
    /// <summary>
    /// The single way the player communicates with external systems. Any external non player related systems goes through this facade
    /// </summary>
    public interface IPlayerFacade {
        public Transform AimPoint { get; }
        public Transform Root { get; }
        public IHealthEvents HealthEvents { get; }
        public IDeathEvents DeathEvents { get; }
        public IAmmoEvents AmmoEvents { get; }
        public void BindServices(PlayerDeps deps);
    }
}
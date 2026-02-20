using UnityEngine;

namespace _Project.Scripts.Actors {
    public interface IPlayerFacade {
        public Transform HeadAnchor { get; }
        public void BindServices(PlayerDeps deps);
    }
}
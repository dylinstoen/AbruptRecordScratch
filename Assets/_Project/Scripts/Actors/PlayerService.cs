using UnityEngine;

namespace _Project.Scripts.Actors {
    public class PlayerService : MonoBehaviour, IPlayerService {
        private IPlayerFacade _playerFacade;
        private MonoBehaviour _playerFacadeBehaviour;
        public IPlayerFacade PlayerFacade => _playerFacade;
        public bool IsDead => !_playerFacadeBehaviour;

        public void Initialize(IPlayerFacade playerFacade) {
            _playerFacade = playerFacade;
            _playerFacadeBehaviour = playerFacade as MonoBehaviour;
        }
    }
}
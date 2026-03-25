using UnityEngine;

namespace _Project.Scripts.Actors {
    public class PlayerService : MonoBehaviour, IPlayerService {
        private IPlayerFacade _playerFacade;
        public IPlayerFacade PlayerFacade => _playerFacade;
        public void Initialize(IPlayerFacade playerFacade) {
            _playerFacade = playerFacade;
        }
    }
}
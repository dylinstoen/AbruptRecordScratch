using _Project.Scripts.UI.Navigation;
using UnityEngine;

namespace _Project.Scripts.UI.Pause {
    public class PauseMenuBackHandler : MonoBehaviour, IMenuBackHandler {
        [SerializeField] private PauseMenuController pauseMenuController;
        public bool TryHandleBack() {
            pauseMenuController.Resume();
            return true;
        }
    }
}


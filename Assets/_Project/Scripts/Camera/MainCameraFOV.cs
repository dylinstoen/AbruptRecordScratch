using _Project.Scripts.GameRoot;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;

namespace _Project.Scripts.Cam {
    public class MainCameraFOV : MonoBehaviour, ISettingsReceiver {
        [SerializeField] private CinemachineCamera cam;
        ISettingsService _settingsService;
        public void Initialize(ISettingsService settingsService) {
            _settingsService = settingsService;
            _settingsService.Register(this);
        }
        public void OnDestroy() {
            _settingsService?.Unregister(this);
        }
        public void ApplySettings(SettingsData settings) {
            cam.Lens.FieldOfView = settings.VerticalFOV;
        }
    }
}


using _Project.Scripts.Input;
using _Project.Scripts.Actors;
using _Project.Scripts.Combat;
using _Project.Scripts.UI;
using _Project.Scripts.UI.DeathScreen;
using _Project.Scripts.UI.Reticle;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class LevelBootstrap : MonoBehaviour {
        [Header("Camera")]
        [SerializeField] private CameraSourceRig cameraSourceRig;
        [SerializeField] private CameraRecoilService cameraRecoilService;
        [Header("Player")]
        [SerializeField] private PlayerIntentSource playerIntentSource;
        [SerializeField] private PlayerSpawnService playerSpawner;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private PlayerConfigSO playerConfigSo;
        [SerializeField] private Transform weaponViewMount;
        [Header("UI")]
        [SerializeField] private WeaponHud weaponHud;
        [SerializeField] private HealthHud healthHud;
        [SerializeField] private DeathScreen deathScreen;
        [SerializeField] private Transform reticleMount;
        [Header("Input")]
        [SerializeField] private InputModeService inputModeService;
        [Header("Services")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private HitService hitService;
        
        private void Start() {
            var player = playerSpawner.Spawn(playerSpawnPoint.position, playerSpawnPoint.rotation);
            cameraSourceRig.SetFollowTarget(player.HeadAnchor);
            player.BindServices(new PlayerDeps {
                PlayerConfigSo = playerConfigSo, 
                AimRaySource = cameraSourceRig, 
                HitService =  hitService,
                LookCameraSource = cameraSourceRig, 
                IntentSource = playerIntentSource, 
                ReticleMount = reticleMount,
                CameraRecoilService =  cameraRecoilService as ICameraRecoilService,
                WeaponViewMount = weaponViewMount
            });
            healthHud.BindHealthEvents(player.HealthEvents);
            weaponHud.BindAmmoEvents(player.AmmoEvents);
            gameManager.Initialize(player.DeathEvents, deathScreen, inputModeService, inputModeService.DeathUIIInputEvent);
        }
    }
}
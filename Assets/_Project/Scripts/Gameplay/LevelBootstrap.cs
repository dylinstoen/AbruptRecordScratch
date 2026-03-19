using _Project.Scripts.Input;
using _Project.Scripts.Actors;
using _Project.Scripts.Audio;
using _Project.Scripts.Cam;
using _Project.Scripts.Combat;
using _Project.Scripts.UI;
using _Project.Scripts.UI.DeathScreen;
using _Project.Scripts.UI.Reticle;
using KBCore.Refs;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class LevelBootstrap : MonoBehaviour {
        [Header("Camera")] 
        [SerializeField] private Camera cameraSource;
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private DeadCamFollower deadCamFollower;
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
        [SerializeField] private InteractionPresenter interactionPresenter;
        [Header("Input")]
        [SerializeField] private InputModeService inputModeService;
        [Header("Services")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ImpactService impactService;
        [SerializeField] private AudioService audioService;
        
        private void Start() {
            var player = playerSpawner.Spawn(playerSpawnPoint.position, playerSpawnPoint.rotation);
            cinemachineCamera.Follow = player.HeadAnchor;
            deadCamFollower.SetTarget(player.HeadAnchor);
            player.BindServices(new PlayerDeps {
                CameraBrain =  cameraSource,
                PlayerConfigSo = playerConfigSo, 
                ImpactService =  impactService,
                IntentSource = playerIntentSource, 
                ReticleMount = reticleMount,
                WeaponViewMount = weaponViewMount,
                InteractionPresenter = interactionPresenter,
                AudioService = audioService
            });
            healthHud.BindHealthEvents(player.HealthEvents);
            weaponHud.BindAmmoEvents(player.AmmoEvents);
            gameManager.Initialize(player.DeathEvents, deathScreen, inputModeService, inputModeService.DeathUIIInputEvent);
        }
    }
}
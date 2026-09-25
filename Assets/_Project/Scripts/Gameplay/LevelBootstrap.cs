using System;
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
using _Project.Scripts.Core.Level;
using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay.Combat.Enemies;
using _Project.Scripts.UI.Pause;
using _Project.Scripts.Gameplay.Interract;
using _Project.Scripts.GameRoot;
using _Project.Scripts.UI.Navigation;

namespace _Project.Scripts.Gameplay {
    public class LevelBootstrap : MonoBehaviour {
        [Header("Level")]
        [SerializeField, Anywhere] private InterfaceRef<ILevelController> levelControllerRef;
        [SerializeField, Anywhere] private InterfaceRef<ILevelStateSource> levelStateSourceRef;
        [SerializeField] private EndLevelTrigger endLevelTrigger;
        [Header("Camera")] 
        [SerializeField] private Camera cameraSource;
        [SerializeField] private CinemachineCamera cinemachineCamera;
        [SerializeField] private DeadCamFollower deadCamFollower;
        [SerializeField] private MainCameraFOV mainCameraFOV;
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
        [SerializeField] private CoinHud coinHud;
        [SerializeField] private LevelCompletedScreen levelCompletedScreen;
        [Header("Pause")]
        [SerializeField] private PauseHud pauseHud;
        [SerializeField] private PauseMenuController pauseMenuController;
        [SerializeField] private MenuPage pausePage;
        [Header("Input")]
        [SerializeField] private InputModeService inputModeService;
        [SerializeField] private MenuInputModeTracker menuInputModeTracker;
        [SerializeField] private GameplayPauseInput gameplayPauseInput;

        [Header("Services")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ImpactService impactService;
        [SerializeField] private AudioService audioService;
        [SerializeField] private EnemySpawnService enemySpawnService;
        [SerializeField, Anywhere] private InterfaceRef<IPlayerService> playerService;
        [SerializeField, Anywhere] private InterfaceRef<ICoinService> coinService;
        [SerializeField] private StainPool stainPool;
        [SerializeField] private SplatterPool splatterPool;
        
        
        private void Start() {
            // Setup
            var player = playerSpawner.Spawn(playerSpawnPoint.position, playerSpawnPoint.rotation);
            cinemachineCamera.Follow = player.AimPoint;
            deadCamFollower.SetTarget(player.AimPoint);
            ISettingsService settingsInScene = GameRoot.GameRoot.Instance.Settings;
            
            player.BindServices(new PlayerDeps {
                CameraBrain = cameraSource,
                PlayerConfigSo = playerConfigSo,
                ImpactService = impactService,
                IntentSource = playerIntentSource,
                ReticleMount = reticleMount,
                WeaponViewMount = weaponViewMount,
                InteractionPresenter = interactionPresenter,
                AudioService = audioService,
                LevelStateSource = levelStateSourceRef.Value,
                levelController = levelControllerRef.Value,
                SettingsService = settingsInScene
            });
            // Bind Services
            healthHud.BindHealthEvents(player.HealthEvents);
            weaponHud.BindAmmoEvents(player.AmmoEvents);
            gameManager.Initialize(player.DeathEvents, deathScreen, inputModeService, inputModeService.DeathUIIInputEvent, levelControllerRef.Value);
            playerService.Value.Initialize(player);
            coinHud.Initalize(coinService.Value);
            stainPool.Initialize(levelStateSourceRef.Value);
            splatterPool.Initialize(levelStateSourceRef.Value);
            enemySpawnService.SpawnEnemies(levelStateSourceRef.Value);
            pauseHud.Initialize(levelStateSourceRef.Value);
            endLevelTrigger.Initialize(levelControllerRef.Value);
            levelCompletedScreen.Initialize(levelStateSourceRef.Value, coinService.Value);
            levelControllerRef.Value.StartLevel();
            mainCameraFOV.Initialize(settingsInScene);

            pausePage.Initialize(menuInputModeTracker);
            pauseMenuController.Initialize(levelControllerRef.Value, levelStateSourceRef.Value, inputModeService, gameplayPauseInput);
        }

        private void OnValidate() => this.ValidateRefs();
    }
}
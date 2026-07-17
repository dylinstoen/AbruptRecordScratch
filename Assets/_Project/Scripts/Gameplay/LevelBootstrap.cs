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
        [Header("Input")]
        [SerializeField] private InputModeService inputModeService;
        [Header("Services")]
        [SerializeField] private GameManager gameManager;
        [SerializeField] private ImpactService impactService;
        [SerializeField] private AudioService audioService;
        [SerializeField] private EnemySpawnService enemySpawnService;
        [SerializeField, Anywhere] private InterfaceRef<IPlayerService> playerService;
        [SerializeField, Anywhere] private InterfaceRef<ICoinService> coinService;
        
        private void Start() {
            var player = playerSpawner.Spawn(playerSpawnPoint.position, playerSpawnPoint.rotation);
            cinemachineCamera.Follow = player.AimPoint;
            deadCamFollower.SetTarget(player.AimPoint);
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
            playerService.Value.Initialize(player);
            coinHud.Initalize(coinService.Value);

            enemySpawnService.SpawnEnemies(levelStateSourceRef.Value);

            endLevelTrigger.Initialize(levelControllerRef.Value);
            levelCompletedScreen.Initialize(levelStateSourceRef.Value, coinService.Value);
            levelControllerRef.Value.StartLevel();
        }

        private void OnValidate() => this.ValidateRefs();
    }
}
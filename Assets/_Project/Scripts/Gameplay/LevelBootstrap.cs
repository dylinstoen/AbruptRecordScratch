using _Project.Scripts.Input;
using _Project.Scripts.Actors;
using _Project.Scripts.UI;
using _Project.Scripts.UI.DeathScreen;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class LevelBootstrap : MonoBehaviour {
        [Header("Player")]
        [SerializeField] private PlayerIntentSource playerIntentSource;
        [SerializeField] private PlayerSpawnService playerSpawner;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private CameraRig cameraRig;
        [SerializeField] private PlayerConfigSO playerConfigSo;
        [Header("UI")]
        [SerializeField] private WeaponHud weaponHud;
        [SerializeField] private HealthHud healthHud;
        [SerializeField] private DeathScreen deathScreen;
        [Header("Input")]
        [SerializeField] private InputModeService inputModeService;
        [Header("Manager")]
        [SerializeField] private GameManager gameManager;
        
        private void Start() {
            // System
            inputModeService.SetGameplay();
            
            
            // Player
            var player = playerSpawner.Spawn(playerSpawnPoint.position, playerSpawnPoint.rotation);
            cameraRig.SetFollowTarget(player.HeadAnchor);
            player.BindServices(new PlayerDeps{PlayerConfigSo = playerConfigSo, CameraRig = cameraRig, IntentSource = playerIntentSource});
            healthHud.BindHealthEvents(player.HealthEvents);
            weaponHud.BindAmmoEvents(player.AmmoEvents);
            gameManager.Initialize(player.DeathEvents, deathScreen, inputModeService, inputModeService.DeathUIIInputEvent);
        }
    }
}
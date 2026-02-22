using _Project.Scripts.Actors;
using _Project.Scripts.Actors.Weapon;
using _Project.Scripts.UI.Weapon;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class LevelBootstrap : MonoBehaviour {
        [SerializeField] private PlayerSpawnService playerSpawner;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private CameraRig cameraRig;
        [SerializeField] private PlayerConfigSO playerConfigSo;
        [Header("HUD")]
        [SerializeField] private WeaponHud weaponHud;
        
        private void Start() {
            var player = playerSpawner.Spawn(playerSpawnPoint.position, playerSpawnPoint.rotation);
            cameraRig.SetFollowTarget(player.HeadAnchor);
            player.BindServices(new PlayerDeps{CameraRig = cameraRig, PlayerConfigSo = playerConfigSo, WeaponViewMount = cameraRig.weaponViewMount, WeaponHud = weaponHud});
        }
    }
}
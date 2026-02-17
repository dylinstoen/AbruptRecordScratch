using _Project.Scripts.Actors;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class LevelBootstrap : MonoBehaviour {
        [SerializeField] private PlayerSpawnService playerSpawner;
        [SerializeField] private Transform playerSpawnPoint;
        [SerializeField] private CameraRig cameraRig;
        [SerializeField] private PlayerConfigSO playerConfigSo;
        
        private void Start() {
            var player = playerSpawner.Spawn(playerSpawnPoint.position, playerSpawnPoint.rotation);
            cameraRig.SetFollowTarget(player.HeadAnchor);
            player.BindServices(new PlayerDeps{CameraRig = cameraRig, PlayerConfigSo = playerConfigSo, WeaponViewMount = cameraRig.weaponViewMount});
        }
    }
}
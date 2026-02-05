using FPS;
using UnityEngine;
using FPS.Character.Player;
using FPS.Weapon;
using UnityEngine.Serialization;
using FPS.Aiming;
using FPS.Character;

namespace FPS.Game {
    public class Bootstrap : MonoBehaviour {
        [Header("Player")]
        // TODO: Create a player factory/builder that hooks player and camera
        [SerializeField] private Character.Player.Controller playerController;
        [SerializeField] private Input.PlayerFireInput playerFireInput;
        [SerializeField] private Transform playerFollowTarget;
        [SerializeField] private Transform playerLookTarget;
        [SerializeField] private WeaponSystem weaponSystem;
        [Header("Camera")]
        [SerializeField] private Camera.Controller cameraController;

        private void Start() {
            HookPlayer();
            HookCamera();
    
        }
        void HookPlayer() {
            playerController.Inject(cameraController);
            weaponSystem.Inject(cameraController, playerFireInput);
        }
        void HookCamera() {
            cameraController.Inject(playerFollowTarget, playerLookTarget);
        }

    }  
}


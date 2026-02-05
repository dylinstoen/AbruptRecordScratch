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
        [Header("Pistol")]
        // Put these into one starting weapon data object
        [SerializeField] private View pistolWeaponView;
        [SerializeField] private Weapon.Controller pistolWeaponController;
        [Header("Rifle")]
        // Put these into one starting weapon data object
        [SerializeField] private View rifleWeaponView;
        [SerializeField] private Weapon.Controller rifleWeaponController;
        
        private void Start() {
            HookPlayer();
            HookCamera();
            HookWeapon();
        }

        void HookPlayer() {
            playerController.Inject(cameraController);
            weaponSystem.Inject(cameraController, playerFireInput);
        }
        void HookCamera() {
            cameraController.Inject(playerFollowTarget, playerLookTarget);
        }
        void HookWeapon() {
            // TODO: Create a pistol factory based on weapon data object
            pistolWeaponController.Inject(cameraController);
            pistolWeaponView.Inject(cameraController);
            rifleWeaponView.Inject(cameraController);
            rifleWeaponController.Inject(cameraController);
        }
    }  
}


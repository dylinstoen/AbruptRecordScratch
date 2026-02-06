using System;
using UnityEngine;

namespace FPS.Character.Player {
    public class Player : MonoBehaviour, IPickupable {
        [SerializeField] private Camera.Controller cameraController;
        [SerializeField] private WeaponSystem weaponSystem;
        [SerializeField] private Controller playerController;

        [Header("Camera Settings")] 
        [SerializeField] private Transform camFollowTarget;
        [SerializeField] private Transform camLookAtTarget;
    
        [Header("Fire Input Settings")]
        [SerializeField] private Input.PlayerFireInput playerFireInput;
    
        private void Start() {
            cameraController.Init(camFollowTarget, camLookAtTarget);
            playerController.Init(cameraController);
            weaponSystem.Init(cameraController, playerFireInput);
        }

        private void Update() {
            playerController.Tick();
            weaponSystem.Tick();
        }

        private void LateUpdate() {
            cameraController.LateTick();
            weaponSystem.LateTick();
        }
    
        private void FixedUpdate() {
            playerController.FixedTick();
        }

        public void Apply() {
            
        }
    }

}

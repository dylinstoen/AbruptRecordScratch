using System;
using _Project.Input;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public sealed class PlayerFacade : MonoBehaviour, IPlayerFacade {
        public Transform HeadAnchor => headAnchor;
        
        [SerializeField] private Transform headAnchor;
        [SerializeField] private Transform weaponLogicMount;
        [SerializeField] private PlayerIntentSource playerIntentSource;
        [SerializeField] private PlayerLookController playerLookController;
        [SerializeField] private ActorMotor actorMotor;
        [SerializeField] private PlayerPause playerPause;
        [SerializeField] private WeaponOwner weaponOwner;
        private void Awake() {
            actorMotor.Initialize(playerIntentSource);
            playerLookController.Initialize(playerIntentSource);
            playerPause.Initialize(playerIntentSource);
        }

        public void BindServices(PlayerDeps deps) {
            actorMotor.BindAimSource(deps.CameraRig);
            playerLookController.BindCamera(deps.CameraRig);
            weaponOwner.BuildWeaponHud(deps.WeaponHud);
            weaponOwner.BuildAmmo(deps.PlayerConfigSo.ammoProfileSo);
            weaponOwner.BuildWeapons(weaponLogicMount, deps.WeaponViewMount, deps.PlayerConfigSo.weaponLoadoutSo);
            weaponOwner.BuildRunner(playerIntentSource, deps.CameraRig);
            Activate();
        }

        private void Activate() {
            actorMotor.enabled = true;
            playerPause.enabled = true;
            playerLookController.enabled = true;
            weaponOwner.enabled = true;
        }
    }
}


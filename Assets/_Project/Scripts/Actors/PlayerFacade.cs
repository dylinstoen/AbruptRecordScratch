using System;
using _Project.Scripts.Input;
using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Gameplay;
using UnityEngine;


namespace _Project.Scripts.Actors {
    public sealed class PlayerFacade : MonoBehaviour, IPlayerFacade {
        public Transform HeadAnchor => headAnchor;
        public IHealthEvents HealthEvents => health;
        public IDeathEvents DeathEvents => playerDeathHandler;
        public IAmmoEvents AmmoEvents => weaponHudPresenter;

        [SerializeField] private Transform headAnchor;
        [SerializeField] private ActorMotor actorMotor;
        [SerializeField] private PlayerLookController playerLookController;
        [SerializeField] private Health health;
        [SerializeField] private PlayerDeathHandler playerDeathHandler;
        [SerializeField] private WeaponHudPresenter weaponHudPresenter;
        [SerializeField] private AmmoInventory ammoInventory;
        [SerializeField] private WeaponOwner weaponOwner;
        
        public void BindServices(PlayerDeps deps) {
            actorMotor.BindIntent(deps.IntentSource);
            playerLookController.BindIntent(deps.IntentSource);
            weaponOwner.BindIntent(deps.IntentSource);
            
            actorMotor.BindAimSource(deps.CameraRig);
            playerLookController.BindCamera(deps.CameraRig);
            health.BindPlayerHealth(deps.PlayerConfigSo.startingHealth);
            ammoInventory.BindAmmoProfile(deps.PlayerConfigSo.ammoProfileSo);
            weaponOwner.BuildWeapons(deps.CameraRig.weaponViewMount, deps.PlayerConfigSo.weaponLoadoutSo);
            weaponOwner.BuildRunner(deps.CameraRig);
        }
    }
}


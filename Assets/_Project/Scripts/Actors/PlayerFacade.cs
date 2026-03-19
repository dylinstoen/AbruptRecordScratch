using UnityEngine;


namespace _Project.Scripts.Actors {
    public sealed class PlayerFacade : MonoBehaviour, IPlayerFacade {
        public Transform HeadAnchor => headAnchor;
        public IHealthEvents HealthEvents => health;
        public IDeathEvents DeathEvents => playerDeathHandler;
        public IAmmoEvents AmmoEvents => weaponHudPresenter;

        [SerializeField] private Transform headAnchor;
        [SerializeField] private PlayerMover playerMover;
        [SerializeField] private PlayerLookController playerLookController;
        [SerializeField] private Health health;
        [SerializeField] private PlayerDeathHandler playerDeathHandler;
        [SerializeField] private WeaponHudPresenter weaponHudPresenter;
        [SerializeField] private AmmoInventory ammoInventory;
        [SerializeField] private WeaponOwner weaponOwner;
        [SerializeField] private PlayerInteraction playerInteraction;
        [SerializeField] private PlayerCamTargetDriver playerCamTargetDriver;
        [SerializeField] private PlayerMoverPresenter playerMoverPresenter;
        
        public void BindServices(PlayerDeps deps) {
            playerCamTargetDriver.Initialize(deps.CameraBrain);
            playerMover.Initialize(deps.IntentSource, playerCamTargetDriver);
            playerLookController.Initialize(deps.IntentSource, playerCamTargetDriver);
            health.Initialize(deps.PlayerConfigSo.startingHealth);
            ammoInventory.Initialize(deps.PlayerConfigSo.ammoProfileSo);
            weaponOwner.Initialize(deps.IntentSource, deps.ImpactService, deps.WeaponViewMount, deps.PlayerConfigSo.weaponLoadoutSo, playerCamTargetDriver, deps.ReticleMount, deps.AudioService);
            playerInteraction.Initialize(deps.InteractionPresenter, deps.IntentSource, playerCamTargetDriver);
            playerMoverPresenter.Initialize(deps.ImpactService);
        }
    }
}


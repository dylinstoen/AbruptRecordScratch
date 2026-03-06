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
        [SerializeField] private PlayerInteraction playerInteraction;
        
        public void BindServices(PlayerDeps deps) {
            actorMotor.Initialize(deps.IntentSource, deps.AimRaySource);
            playerLookController.Initialize(deps.IntentSource, deps.LookCameraSource);
            health.Initialize(deps.PlayerConfigSo.startingHealth);
            ammoInventory.Initialize(deps.PlayerConfigSo.ammoProfileSo);
            weaponOwner.Initialize(deps.IntentSource, deps.HitService, deps.WeaponViewMount, deps.PlayerConfigSo.weaponLoadoutSo, deps.AimRaySource, deps.ReticleMount, deps.CameraRecoilService);
            playerInteraction.Initialize(deps.InteractionPresenter, deps.IntentSource, deps.AimRaySource);
        }
    }
}


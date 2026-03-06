using _Project.Scripts.Combat;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Input;
using _Project.Scripts.UI.Reticle;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public struct PlayerDeps {
        public IAimRaySource AimRaySource;
        public ILookCameraSource LookCameraSource;
        public ICameraRecoilService CameraRecoilService;
        public IHitService HitService;
        public PlayerConfigSO PlayerConfigSo;
        public IIntentSource IntentSource;
        public Transform ReticleMount;
        public Transform WeaponViewMount;
        public IInteractionPresenter InteractionPresenter;
    }
}
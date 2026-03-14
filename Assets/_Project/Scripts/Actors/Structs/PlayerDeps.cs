using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Combat;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Input;
using _Project.Scripts.UI.Reticle;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public struct PlayerDeps {
        public Camera CameraBrain;
        public IImpactService ImpactService;
        public PlayerConfigSO PlayerConfigSo;
        public IIntentSource IntentSource;
        public Transform ReticleMount;
        public Transform WeaponViewMount;
        public IInteractionPresenter InteractionPresenter;
        public IAudioService AudioService;
    }
}
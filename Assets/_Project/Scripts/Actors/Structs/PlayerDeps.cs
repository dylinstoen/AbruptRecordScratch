using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Combat;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Input;
using _Project.Scripts.UI.Reticle;
using UnityEngine;

namespace _Project.Scripts.Actors {
    /// <summary>
    /// Passed by the Level Service and contains every external system the player depends on to get running. Must pass all of these for a player to function
    /// </summary>
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
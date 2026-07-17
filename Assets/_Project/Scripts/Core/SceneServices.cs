using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Combat;
using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Core {
    /// <summary>
    /// For Game objects that already exist in the scene and need access to global services
    /// </summary>
    public sealed class SceneServices : MonoBehaviour {
        [Header("Gameplay")]
        [SerializeField] private MonoBehaviour impactService;
        [SerializeField] private MonoBehaviour audioService;
        [SerializeField] private MonoBehaviour playerService;
        [SerializeField] private MonoBehaviour coinService;

        public IImpactService Impact => (IImpactService)impactService;
        public IAudioService Audio => (IAudioService)audioService;
        public IPlayerService Player => (IPlayerService)playerService;
        public ICoinService Coin => (ICoinService)coinService;

        private void Awake() {
            if (!impactService)
                throw new InvalidOperationException($"{name} 'hit service must be assigned");
            if (impactService is not IImpactService) throw new InvalidOperationException($"{name}: 'impactService' must implement IImpactService.");
            if (!audioService)
                throw new InvalidOperationException($"{name} 'audio service must be assigned");
            if (audioService is not IAudioService) throw new InvalidOperationException($"{name}: 'audioService' must implement IAudioService.");
            if (!playerService)
                throw new InvalidOperationException($"{name} 'player service must be assigned");
            if(playerService is not IPlayerService) throw new InvalidOperationException($"{name}: 'playerService' must implement IPlayerFacade.");
            if (!coinService)
                throw new InvalidOperationException($"{name} 'coin service must be assigned");
            if (coinService is not ICoinService) throw new InvalidOperationException($"{name}: 'coinService' must implement ICoinService.");
            SceneServiceLocator.Bind(this);
        }

        private void OnDestroy() {
            SceneServiceLocator.Unbind(this);
        }
    }
}
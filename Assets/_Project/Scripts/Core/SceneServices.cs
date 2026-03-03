using System;
using _Project.Scripts.Combat;
using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Core {
    /// <summary>
    /// For Game objects that already exist in the scene and need access to global services
    /// </summary>
    public sealed class SceneServices : MonoBehaviour {
        [Header("Gameplay")]
        [SerializeField] private MonoBehaviour hitService;
        
        public IHitService Hit => (IHitService)hitService;

        private void Awake() {
            if (!hitService)
                throw new InvalidOperationException($"{name} 'hit service must be assigned");
            if (hitService is not IHitService) throw new InvalidOperationException($"{name}: 'impactService' must implement IImpactService.");
            SceneServiceLocator.Bind(this);
        }

        private void OnDestroy() {
            SceneServiceLocator.Unbind(this);
        }
    }
}
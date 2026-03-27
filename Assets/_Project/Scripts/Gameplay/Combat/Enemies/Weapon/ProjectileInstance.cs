using System;
using _Project.Scripts.Gameplay;
using UnityEngine;

namespace _Project.Scripts.Combat.Weapon {
    public class ProjectileInstance : MonoBehaviour {
        [SerializeField] private float speed = 10f;
        private IImpactService _impactService;
        private SourceVisualImpactProfileSO _sourceVisualImpactProfile;
        private SourceAudioImpactProfileSO _sourceAudioImpactProfile;
        private bool _initialized = false;
        public void Initialize(IImpactService impactService, SourceVisualImpactProfileSO sourceVisualImpactProfile, 
            SourceAudioImpactProfileSO sourceAudioImpactProfile, float range) {
            _impactService = impactService;
            _sourceVisualImpactProfile = sourceVisualImpactProfile;
            _sourceAudioImpactProfile = sourceAudioImpactProfile;
            _initialized = true;
        }

        private void Update() {
            if (!_initialized) return;
            
        }
    }
}
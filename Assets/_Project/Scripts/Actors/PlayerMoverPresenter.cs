using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class PlayerMoverPresenter : MonoBehaviour {
        private IImpactService _impactService;
        [SerializeField] private SourceAudioImpactProfileSO sourceAudioImpactProfile;
        [SerializeField] private float moveInterval = 0.5f;
        private float _coolDownRemaining = 0.0f;
        private bool _movingLastFrame = false;
        private bool _initialized = false;
        
        public void Initialize(IImpactService impactService) {
            _impactService = impactService;
            _initialized = true;
        }

        public void Tick(bool moving, float deltaTime, Collider collider) {
            if (!_initialized)
                return;

            bool startedMoving = moving && !_movingLastFrame;
            _movingLastFrame = moving;

            if (_coolDownRemaining > 0f)
                _coolDownRemaining -= deltaTime;

            if (!moving)
                return;
            if (startedMoving && _coolDownRemaining <= 0f) {
                PlayMoveAudio(collider);
                _coolDownRemaining = moveInterval;
                return;
            }

            if (_coolDownRemaining > 0f)
                return;
            
            PlayMoveAudio(collider);
            _coolDownRemaining = moveInterval;
        }
        
        private void PlayMoveAudio(Collider ground) {
            if (!ground)
                return;
            Vector3 hitPoint = ground.ClosestPoint(transform.position);
            Vector3 hitNormal = (transform.position - ground.transform.position).normalized;
            HitContext  hitContext = new HitContext(hitPoint, hitNormal, ground, gameObject, 0);
            _impactService.ProcessHitAudio(hitContext, sourceAudioImpactProfile);
        }
    }
}
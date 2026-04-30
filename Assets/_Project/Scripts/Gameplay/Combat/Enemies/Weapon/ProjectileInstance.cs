using System;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Utilities;
using UnityEngine;

namespace _Project.Scripts.Combat.Weapon {
    public class ProjectileInstance : MonoBehaviour {
        [SerializeField] private float speed = 10f;
        private IImpactService _impactService;
        [SerializeField] private SourceVisualImpactProfileSO _sourceVisualImpactProfile;
        [SerializeField] private SourceAudioImpactProfileSO _sourceAudioImpactProfile;
        [SerializeField] private float _timeToLive = 5f;
        private bool _initialized = false;
        private Vector3 _startingPosition = Vector3.zero;
        private float _range = 0f;

        private CountdownTimer _countdownTimer;
        
        public void Initialize(IImpactService impactService, float range) {
            _impactService = impactService;
            _initialized = true;
            _startingPosition = transform.position;
            _range = range;
            _countdownTimer = new CountdownTimer(_timeToLive);
            _countdownTimer.Start();
        }

        private void Update() {
            if (!_initialized) return;
            if(Vector3.Distance(_startingPosition, transform.position) >= _range || _countdownTimer.IsFinished) {
                Destroy(gameObject);
                return;
            }
            transform.position += transform.forward * speed * Time.deltaTime;
        }
        
        private void OnCollisionEnter(Collision collision) {
            // TODO" Hit something, create impact
            
            Debug.Log("collided with " + collision.gameObject.name);
            Destroy(gameObject);
        }
    }
}
using System;
using _Project.Scripts.Core.Level.Interface;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Utilities;
using _Project.Scripts.Gameplay.Enums;

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
        private ILevelStateSource _levelStateSource;

        private CountdownTimer _countdownTimer;
        
        public void Initialize(ILevelStateSource levelStateSource, IImpactService impactService, float range) {
            _impactService = impactService;
            _initialized = true;
            _startingPosition = transform.position;
            _levelStateSource = levelStateSource;
            _range = range;
            _countdownTimer = new CountdownTimer(_timeToLive);
            _countdownTimer.Start();
        }

        private void Update() {
            Debug.Log(_levelStateSource.CurrentState.ToString());
            if (!_initialized || _levelStateSource == null || _levelStateSource.CurrentState != LevelState.Playing) return;
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
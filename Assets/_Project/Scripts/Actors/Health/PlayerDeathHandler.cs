using System;
using UnityEngine;

namespace _Project.Scripts.Actors {
    [RequireComponent(typeof(Health))]
    public class PlayerDeathHandler : MonoBehaviour, IDeathEvents, IDisposable {
        private Health _health;
        public event Action Died;
        private void Awake() {
            _health = GetComponent<Health>();
            _health.InternalDied += OnInternalDied;
        }

        private void OnDestroy() {
            Dispose();
        }

        private void OnInternalDied() {
            Died?.Invoke();
            Destroy(gameObject);
        }

        public void Dispose() {
            _health.InternalDied -= OnInternalDied;
        }

        
    }
}
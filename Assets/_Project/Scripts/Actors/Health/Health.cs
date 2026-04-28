using System;
using _Project.Scripts.Gameplay.Combat.Struct;
using _Project.Scripts.Gameplay.Structs;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class Health : MonoBehaviour, IHealthEvents, IHealthAcquirer, IDamageable {
        [SerializeField] private int _maxHealth = 100;
        private int _currentHealth;
        public event Action<int, int> HealthChanged;
        public event Action InternalDied;
        
        private void Start() {
            _currentHealth = _maxHealth;
        }
        
        public void Initialize(int startingHealth) {
            _maxHealth = startingHealth;
            HealthChanged?.Invoke(startingHealth, _maxHealth);
        }
        
        public void ApplyDamage(in HitContext ctx) {
            if (_currentHealth <= 0) return;
            _currentHealth -= ctx.Damage;
            _currentHealth = Mathf.Max(0, _currentHealth);
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
            if(_currentHealth == 0) InternalDied?.Invoke();
        }

        public bool AddHealth(int amount) {
            if(_currentHealth == _maxHealth) return false;
            _currentHealth = Mathf.Min(amount + _currentHealth, _maxHealth);
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
            return true;
        }
    }
}
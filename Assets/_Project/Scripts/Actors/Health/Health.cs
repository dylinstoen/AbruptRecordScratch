using System;
using _Project.Scripts.Gameplay.Combat.Struct;
using _Project.Scripts.UI;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public class Health : MonoBehaviour, IHealthEvents, IDamageable {
        private int _maxHealth = 100;
        private int _currentHealth;
        public event Action<int, int> HealthChanged;
        public event Action InternalDied;
        
        private void Start() {
            _currentHealth = _maxHealth;
        }
        
        public void BindPlayerHealth(int startingHealth) {
            _maxHealth = startingHealth;
        }
        
        public void ApplyDamage(in DamageContext ctx) {
            if (_currentHealth <= 0) return;
            _currentHealth -= ctx.Amount;
            _currentHealth = Mathf.Max(0, _currentHealth);
            HealthChanged?.Invoke(_currentHealth, _maxHealth);
            if(_currentHealth == 0) InternalDied?.Invoke();
        }
    }
}
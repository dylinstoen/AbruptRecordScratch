using UnityEngine;

namespace FPS.Player {
    public class Player : MonoBehaviour, IDamageable {
        public int health;
        public int maxHealth;
    
        public int Health => health;
        public int MaxHealth => maxHealth;

        public void TakeDamage(float amount) {
            throw new System.NotImplementedException();
        }
    
        public void Die() {
            throw new System.NotImplementedException();
        }
    }
}


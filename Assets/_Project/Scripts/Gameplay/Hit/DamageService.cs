using _Project.Scripts.Actors;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public class DamageService : MonoBehaviour {
        public void ApplyDamage(in HitContext ctx, IDamageable damageable) {
            damageable.ApplyDamage(ctx);
        }
    }
}
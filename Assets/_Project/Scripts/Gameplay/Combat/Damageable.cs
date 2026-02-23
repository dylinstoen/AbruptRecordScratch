using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Gameplay.Combat.Struct;
using UnityEngine;

namespace _Project.Scripts.Interactables {
    public class Damageable : MonoBehaviour {
        [SerializeField] private int amount;
        private void OnCollisionEnter(Collision other) {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable == null) return;
            var contact = other.contactCount > 0 ? other.GetContact(0) : default;
            var hitPoint = contact.point;
            Vector3 hitNormal = contact.normal;
            var ctx = new DamageContext(amount, hitNormal, hitPoint, this.gameObject);
            damageable.ApplyDamage(in ctx);
        }
    }
}


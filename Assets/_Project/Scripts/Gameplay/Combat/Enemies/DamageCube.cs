using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Core;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Gameplay.Combat.Struct;
using _Project.Scripts.Gameplay.Structs;
using UnityEngine;

namespace _Project.Scripts.Combat {
    public class DamageCube : MonoBehaviour {
        [SerializeField] private int amount;
        
        private IHitService _hitService;
        private void Awake() {
            _hitService = SceneServiceLocator.Current.Hit;
        }

        private void OnCollisionEnter(Collision other) {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable == null) return;
            var contact = other.contactCount > 0 ? other.GetContact(0) : default;
            var ctx = new HitContext(contact.point, contact.normal, other.collider, gameObject, amount, damageable);
            _hitService.ProcessHit(in ctx, null);
        }
    }
}


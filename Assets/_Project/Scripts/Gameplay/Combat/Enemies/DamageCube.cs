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
        
        private IImpactService _impactService;
        private void Awake() {
            _impactService = SceneServiceLocator.Current.Impact;
        }

        private void OnTriggerEnter(Collider other) {
            IDamageable damageable = other.gameObject.GetComponent<IDamageable>();
            if (damageable == null) return;
            Vector3 hitPoint = other.ClosestPoint(transform.position);
            Vector3 hitNormal = (other.transform.position - transform.position).normalized;
            var ctx = new HitContext(hitPoint, hitNormal, other, gameObject, amount, damageable);
            _impactService.ProcessHit(in ctx, null);
        }
    }
}


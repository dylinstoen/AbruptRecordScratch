using System;
using UnityEngine;

namespace _Project.Scripts.Items {
    public sealed class WorldPickup : MonoBehaviour {
        [SerializeField] private ItemDefinition item;
        [SerializeField] private LayerMask allowedLayers;
        [SerializeField] private bool destroyOnApply = true;

        private void OnTriggerEnter(Collider other) {
            if (((1 << other.gameObject.layer) & allowedLayers) == 0) return;
            var target =  other.attachedRigidbody != null ? other.attachedRigidbody.gameObject : gameObject;
            if (item != null && item.TryApply(target)) {
                if(destroyOnApply)
                    Destroy(gameObject);
            }
        }
    }
}
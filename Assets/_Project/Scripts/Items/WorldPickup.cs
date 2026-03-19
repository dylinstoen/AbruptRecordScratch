using System;
using System.ComponentModel.Design;
using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Core;
using UnityEngine;

namespace _Project.Scripts.Items {
    public sealed class WorldPickup : MonoBehaviour {
        [SerializeField] private ItemDefinition item;
        [SerializeField] private LayerMask allowedLayers;
        [SerializeField] private bool destroyOnApply = true;

        private void OnCollisionEnter(Collision other) {
            Debug.Log(other.gameObject.name);
        }

        private void OnTriggerEnter(Collider other) {
            if (((1 << other.gameObject.layer) & allowedLayers) == 0) return;
            var target =  other.attachedRigidbody != null ? other.attachedRigidbody.gameObject : other.gameObject;
            if (!item || !item.TryApply(target, SceneServiceLocator.Current.Audio)) return;
            if(destroyOnApply)
                Destroy(gameObject);
        }
    }
}
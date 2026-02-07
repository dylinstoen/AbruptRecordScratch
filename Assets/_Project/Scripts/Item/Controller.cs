using System;
using System.Collections.Generic;
using FPS.Character;
using UnityEngine;

namespace FPS.Item {
    public class Controller : MonoBehaviour {
        // [Serializable] List<Effect> effects;
        private void OnTriggerEnter(Collider other) {
            IPickupable o;
            if (other.TryGetComponent<IPickupable>(out o)) {
                
            }
        }
    }
}


using UnityEngine;

namespace FPS.Weapon {
    // Defines the firing policy of a weapon
    // Rep Inv:
    // - Inputs the state of the trigger and the time elasped
    // - Returns if a shot can be taken
    public abstract class Mode : ScriptableObject {
        
        public abstract bool CanFire(bool triggerState, float deltaTime);
    }
}


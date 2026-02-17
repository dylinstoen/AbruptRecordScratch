using UnityEngine;

namespace _Project.Scripts.Items {
    public abstract class ItemDefinition : ScriptableObject {
        public abstract bool TryApply(GameObject target);
    }
}
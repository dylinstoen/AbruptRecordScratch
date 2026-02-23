using UnityEngine;

namespace _Project.Scripts.Items {
    [CreateAssetMenu(menuName = "Item/HealthPickup",  fileName = "HealthPickup")]
    public class HealthPickupItemDefinition : ItemDefinition {
        [SerializeField] private float amount;
        public override bool TryApply(GameObject target) {
            // TODO: Implement health pickup
            return true;
        }
    }
}
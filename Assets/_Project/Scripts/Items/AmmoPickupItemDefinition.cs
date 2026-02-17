using UnityEngine;

namespace _Project.Scripts.Items {
    [CreateAssetMenu(fileName = "AmmoPickup", menuName = "Item/AmmoPickup")]
    public class AmmoPickupItemDefinition : ItemDefinition {
        public override bool TryApply(GameObject target) {
            throw new System.NotImplementedException();
        }
    }
}
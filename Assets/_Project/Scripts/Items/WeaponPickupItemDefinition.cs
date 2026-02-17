using UnityEngine;

namespace _Project.Scripts.Items {
    [CreateAssetMenu(fileName = "WeaponPickup", menuName = "Item/WeaponPickup")]
    public class WeaponPickupItemDefinition : ItemDefinition {
        public override bool TryApply(GameObject target) {
            throw new System.NotImplementedException();
        }
    }
}
using _Project.Scripts.Actors;
using UnityEngine;

namespace _Project.Scripts.Items {
    [CreateAssetMenu(menuName = "Item/HealthPickup",  fileName = "HealthPickup")]
    public class HealthPickupItemDefinition : ItemDefinition {
        [SerializeField] private int amount;
        public override bool TryApply(GameObject target) {
            var healthAcquirer = target.GetComponent<IHealthAcquirer>();
            if(healthAcquirer == null) return false;
            return healthAcquirer.AddHealth(amount);
        }
    }
}
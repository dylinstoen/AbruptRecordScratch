using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Enums;
using UnityEngine;

namespace _Project.Scripts.Items {
    [CreateAssetMenu(fileName = "AmmoPickup", menuName = "Item/AmmoPickup")]
    public class AmmoPickupItemDefinition : ItemDefinition {
        [SerializeField] private AmmoType ammoType;
        [SerializeField] private int ammoToAdd;
        public override bool TryApply(GameObject target) {
            var ammoAcquirer = target.GetComponent<IAmmoAcquirer>();
            if (ammoAcquirer == null) return false;
            if (ammoAcquirer.TryAddAmmo(ammoType, ammoToAdd)) return true;
            Debug.LogWarning("Ammo full" + target.name);
            return false;
        }
    }
}
    
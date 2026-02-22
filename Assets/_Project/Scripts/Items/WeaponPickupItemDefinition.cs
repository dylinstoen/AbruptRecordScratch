using _Project.Scripts.Actors;
using _Project.Scripts.Weapon;
using UnityEngine;

namespace _Project.Scripts.Items {
    [CreateAssetMenu(fileName = "WeaponPickup", menuName = "Item/WeaponPickup")]
    public class WeaponPickupItemDefinition : ItemDefinition {
        [SerializeField] private WeaponSO weaponDefinition;
        public override bool TryApply(GameObject target) {
            var weaponInventory = target.GetComponent<WeaponInventory>();
            if (!weaponInventory) return false;
            return true;
        }
    }
}
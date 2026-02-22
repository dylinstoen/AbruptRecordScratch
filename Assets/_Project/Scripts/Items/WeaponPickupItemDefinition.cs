using _Project.Scripts.Actors;
using _Project.Scripts.Weapon;
using UnityEngine;

namespace _Project.Scripts.Items {
    [CreateAssetMenu(fileName = "WeaponPickup", menuName = "Item/WeaponPickup")]
    public class WeaponPickupItemDefinition : ItemDefinition {
        [SerializeField] private WeaponSO weaponDefinition;
        public override bool TryApply(GameObject target) {
            var weaponAcquirer = target.GetComponent<IWeaponAcquirer>();
            if (weaponAcquirer == null) return false;
            if (!weaponAcquirer.TryAddWeapon(weaponDefinition)) {
                Debug.LogWarning("Cant add weapon to " + target.name);
                return false;
            }
            return true;
        }
    }
}
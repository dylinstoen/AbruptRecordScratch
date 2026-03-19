using _Project.Scripts.Actors;
using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Weapon;
using UnityEngine;

namespace _Project.Scripts.Items {
    [CreateAssetMenu(fileName = "WeaponPickup", menuName = "Item/WeaponPickup")]
    public class WeaponPickupItemDefinition : ItemDefinition {
        [SerializeField] private WeaponSO weaponDefinition;
        [SerializeField] private AudioCue audioCue;
        public override bool TryApply(GameObject target, IAudioService audioService) {
            var weaponAcquirer = target.GetComponent<IWeaponAcquirer>();
            if (weaponAcquirer == null) return false;
            if (!weaponAcquirer.TryAddWeapon(weaponDefinition)) return false;
            audioService.Play3D(target.transform.position, target.transform.rotation, audioCue);
            return true;
        }
    }
}
using _Project.Scripts.Actors;
using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Weapon.Enums;
using UnityEngine;

namespace _Project.Scripts.Items {
    [CreateAssetMenu(fileName = "AmmoPickup", menuName = "Item/AmmoPickup")]
    public class AmmoPickupItemDefinition : ItemDefinition {
        [SerializeField] private AmmoType ammoType;
        [SerializeField] private int ammoToAdd;
        [SerializeField] private AudioCue audioCue;
        public override bool TryApply(GameObject target, IAudioService audioService) {
            var ammoAcquirer = target.GetComponent<IAmmoAcquirer>();
            if (ammoAcquirer == null) return false;
            if (!ammoAcquirer.TryAddAmmo(ammoType, ammoToAdd)) return false;
            audioService.Play3D(target.transform.position, target.transform.rotation, audioCue);
            return true;
        }
    }
}
    
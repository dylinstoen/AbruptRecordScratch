using _Project.Scripts.Actors;
using _Project.Scripts.Audio.Interfaces;
using _Project.Scripts.Audio.ScriptableObjects;
using UnityEngine;

namespace _Project.Scripts.Items {
    [CreateAssetMenu(menuName = "Item/HealthPickup",  fileName = "HealthPickup")]
    public class HealthPickupItemDefinition : ItemDefinition {
        [SerializeField] private int amount;
        [SerializeField] private AudioCue audioCue;
        public override bool TryApply(GameObject target, IAudioService audioService) {
            var healthAcquirer = target.GetComponent<IHealthAcquirer>();
            if(healthAcquirer == null) return false;
            if (!healthAcquirer.AddHealth(amount)) return false;
            audioService.Play3D(target.transform.position, target.transform.rotation, audioCue);
            return true;
        }
    }
}
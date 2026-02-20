using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "Raycast", menuName = "Weapon/Emitters/Raycast")]
    public class RaycastEmitterModeSo : EmitterModeSO {
        [SerializeField] private float maxDistance;
        public override IEmitterMode Create() {
            return new RaycastEmitterMode(maxDistance);
        }
    }
}
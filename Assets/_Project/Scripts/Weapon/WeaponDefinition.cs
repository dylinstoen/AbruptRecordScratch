using _Project.Scripts.Weapon.Enums;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "WeaponDefinition", menuName = "Weapon/WeaponDefinition")]
    public sealed class WeaponDefinition : ScriptableObject {
        public AmmoType ammoType;
        public int magSize;
        public GameObject viewPrefab;
        public GameObject motorPrefab;
        public FireModeSO fireMode;
        public EmitterModeSO emitterMode;
        public LayerMask hitLayerMask;
    }
}

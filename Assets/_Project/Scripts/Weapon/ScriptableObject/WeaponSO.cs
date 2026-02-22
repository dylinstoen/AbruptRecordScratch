using _Project.Scripts.Weapon.Enums;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon")]
    public sealed class WeaponSO : ScriptableObject {
        public string iD;
        public AmmoType ammoType;
        public int magSize;
        public int costPerShot;
        public float reloadDuration;
        public GameObject viewPrefab;
        public GameObject motorPrefab;
        public FireModeSO fireMode;
        public EmitterModeSO emitterMode;
        public LayerMask hitLayerMask;
    }
}

using _Project.Scripts.Gameplay;
using _Project.Scripts.Weapon.Enums;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapon/Weapon")]
    public sealed class WeaponSO : ScriptableObject {
        public string iD;
        [Header("Knobs")]
        public AmmoType ammoType;
        public int magSize;
        public int costPerShot;
        public float reloadDuration;
        public float fireRate;
        public int damage;
        [Header("Prefabs")]
        public GameObject viewPrefab;
        public GameObject motorPrefab;
        public GameObject reticlePrefab;
        [Header("Scriptable Objects")]
        public FireModeSO fireMode;
        public EmitterModeSO emitterMode;
        public RecoilSO recoil;
        public SourceImpactProfileSO sourceImpactProfile;
        [Header("Layer Masks")]
        public LayerMask hitLayerMask;
    }
}

using _Project.Scripts.Audio.ScriptableObjects;
using _Project.Scripts.Gameplay;
using _Project.Scripts.Weapon.Enums;
using _Project.Scripts.Weapon.Struct;
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
        public float spread;
        public RecoilProfile recoilProfile;
        
        [Header("Prefabs")]
        public GameObject viewPrefab;
        public GameObject motorPrefab;
        public GameObject reticlePrefab;
        [Header("Scriptable Objects")]
        public FireModeSO fireMode;
        public EmitterModeSO emitterMode;
        public AudioCue gunShotSfx;
        public AudioCue reloadSfx;
        public SourceVisualImpactProfileSO sourceVisualImpactProfile;
        [Header("Layer Masks")]
        public LayerMask hitLayerMask;
    }

}

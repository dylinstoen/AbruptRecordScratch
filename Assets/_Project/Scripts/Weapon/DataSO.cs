using FPS.Weapon.Enums;
using UnityEngine;

namespace FPS.Weapon {
    [CreateAssetMenu(fileName = "Data", menuName = "Weapon/Data")]
    public class DataSO : ScriptableObject {
        public float Range;
        public int Ammo;
        public GameObject projectile;
        public Enums.FireMode FireMode;
        public EmitterMode emitterMode;
    }
}

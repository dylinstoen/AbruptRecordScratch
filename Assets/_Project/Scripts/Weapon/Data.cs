using UnityEngine;

namespace FPS.Weapon {
    [CreateAssetMenu(fileName = "Data", menuName = "Weapon/Data")]
    public class Data : ScriptableObject {
        public float Range;
        public int Ammo;
        public Mode mode;
        public Emitter emitter;
    }
}

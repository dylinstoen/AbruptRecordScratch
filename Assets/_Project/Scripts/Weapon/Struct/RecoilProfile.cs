using UnityEngine;

namespace _Project.Scripts.Weapon.Struct {
    [System.Serializable]
    public struct RecoilProfile {
        public float force;
        public Vector2 velXRange;
        public Vector2 velYRange;
        public Vector2 velZRange;
    }
}
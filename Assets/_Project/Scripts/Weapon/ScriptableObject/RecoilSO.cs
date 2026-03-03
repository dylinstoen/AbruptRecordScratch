using UnityEngine;

namespace _Project.Scripts.Weapon {
    [CreateAssetMenu(menuName = "Weapon/RecoilProfile",  fileName = "RecoilProfile")]
    public class RecoilSO : ScriptableObject {
        [Header("Return")]
        [Min(0.0001f)] public float aimReturnTime = 0.10f;
        public float maxAimSpeed = Mathf.Infinity;
        [Header("Pitch")]
        public float pitchPerShot = 1.6f;
        [Header("Yaw range")]
        public float yawMin = -0.35f;
        public float yawMax = 0.35f;
    }
}
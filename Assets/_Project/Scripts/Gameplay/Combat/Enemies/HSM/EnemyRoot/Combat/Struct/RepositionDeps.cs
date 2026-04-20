using UnityEngine;

namespace _Project.Scripts.Combat.HSM {
    [System.Serializable]
    public struct RepositionDeps {
        public enum RepositionType{Melee, Ranged}
        public enum RepositionDirection{Left, Right}
        public RepositionType repositionType;
        public Vector2 repositionRange;
        public float timeToLive;
        [Header("Ranged")]
        [Tooltip("Range from the player that the Entity is allowed to reposition too. Only for Ranged types.")]
        public float flexibility;
        [Header("Melee")]
        public RepositionDirection repositionDirection;
        public float repositionRadiusAroundTarget;
        
    }
}
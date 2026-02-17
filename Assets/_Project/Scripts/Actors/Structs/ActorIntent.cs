namespace _Project.Scripts.Actors {
    using UnityEngine;
    /// <summary>
    /// A snapshot of the intent of the actor
    /// produced by intent sources
    /// consumed by weapon, movement, etc
    /// </summary>
    public struct ActorIntent {
        public Vector2 Move;
        public Vector2 Look;
        public bool FireHeld;
        public bool FirePressed;
        public float SwitchDelta;
        public bool HasMoveInput => Move != Vector2.zero;
        public bool HasLookInput => Look != Vector2.zero;
        public static ActorIntent None => default;
    }
}
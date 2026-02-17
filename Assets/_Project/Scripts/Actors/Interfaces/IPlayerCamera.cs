namespace _Project.Scripts.Actors {
    using UnityEngine;
    public interface IPlayerCamera {
        public void SetFollowTarget(Transform target);
        public void SetLookInput(Vector2 lookInput);
    }
}
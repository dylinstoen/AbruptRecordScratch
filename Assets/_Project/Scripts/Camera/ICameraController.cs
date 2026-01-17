using UnityEngine;

namespace FPS.Camera {
    public interface ICameraController
    {
        public void Initialize(Transform followTarget, Transform lookAtTarget);
        public void FollowTarget();
        public void LookAtTarget();
        public Transform CamTransform { get; }
    }
}


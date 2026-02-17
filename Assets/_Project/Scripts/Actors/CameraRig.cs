using UnityEngine;

namespace _Project.Scripts.Actors {
    public sealed class CameraRig : MonoBehaviour, IAimRaySource, IPlayerCamera
    {  
        [SerializeField] private Camera cam;
        public Transform weaponViewMount;
        private Transform _follow;
        private Vector2 _yawPitch;
 

        public void LateUpdate() {
            if (_follow) transform.position = _follow.position;
            transform.rotation = Quaternion.Euler(-_yawPitch.y, _yawPitch.x, 0f);
        }

        public void SetFollowTarget(Transform target) => _follow = target;

        public void SetLookInput(Vector2 lookDelta) {
            _yawPitch += lookDelta * 0.1f;
            _yawPitch.y = Mathf.Clamp(_yawPitch.y, -80f, 80f);
        }

        public Ray GetAimRay() => new Ray(cam.transform.position, cam.transform.forward);
    }
}


using _Project.Scripts.Weapon;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public sealed class CameraSourceRig : MonoBehaviour, IAimRaySource, ILookCameraSource
    {  
        [SerializeField] private Camera cam;
        
        [Header("Look")]
        [SerializeField] private float lookSensitivity = 0.1f;
        [SerializeField] private float minPitch = -80f;
        [SerializeField] private float maxPitch =  80f;
        
        private Transform _follow;
        private Vector2 _baseYawPitch;
        private Vector2 _recoilOffset;
        
        public void SetFollowTarget(Transform target) => _follow = target;
        
        public void SetLookInput(Vector2 lookDelta) => AddToBaseYawPitch(lookDelta * lookSensitivity);
        
        public void SetRecoilOffset(Vector2 recoilYawPitch) => _recoilOffset = recoilYawPitch;
        
        public void AddToBaseYawPitch(Vector2 deltaYawPitch) {
            _baseYawPitch += deltaYawPitch;
            _baseYawPitch.y = Mathf.Clamp(_baseYawPitch.y, minPitch, maxPitch);
        }
        
        private void LateUpdate() {
            if (_follow) transform.position = _follow.position;

            Vector2 final = _baseYawPitch + _recoilOffset;
            transform.rotation = Quaternion.Euler(-final.y, final.x, 0f);
        }
        
        public Ray GetAimRay() => new Ray(cam.transform.position, cam.transform.forward);
    }
}


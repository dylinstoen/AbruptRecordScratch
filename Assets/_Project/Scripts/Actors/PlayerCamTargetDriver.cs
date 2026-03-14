using UnityEngine;

namespace _Project.Scripts.Actors {
    public class PlayerCamTargetDriver : MonoBehaviour, IAimRaySource, ILookCameraSource {
        [Header("References")]
        [SerializeField] private Transform yawRoot;
        [SerializeField] private Transform pitchPivot;

        [Header("Look")]
        [SerializeField] private float lookSensitivity = 0.1f;
        [SerializeField] private float minPitch = -80f;
        [SerializeField] private float maxPitch = 80f;
        
        private Vector2 _baseYawPitch;
        private Vector2 _recoilOffset;
        private Camera _camera;
        private bool _initialized = false;

        public void Initialize(Camera cam) {
            _camera = cam;
            _initialized = true;   
        }

        public void SetFollowTarget(Transform target) { }

        public void SetLookInput(Vector2 lookDelta) => AddToBaseYawPitch(lookDelta * lookSensitivity);

        public void SetRecoilOffset(Vector2 recoilYawPitch) => _recoilOffset = recoilYawPitch;

        public void AddToBaseYawPitch(Vector2 deltaYawPitch)
        {
            _baseYawPitch += deltaYawPitch;
            _baseYawPitch.y = Mathf.Clamp(_baseYawPitch.y, minPitch, maxPitch);
        }

        private void LateUpdate()
        {
            if (yawRoot == null || pitchPivot == null)
                return;
            Vector2 final = _baseYawPitch + _recoilOffset;
            yawRoot.rotation = Quaternion.Euler(0f, final.x, 0f);
            pitchPivot.localRotation = Quaternion.Euler(-final.y, 0f, 0f);
        }

        public Ray GetAimRay()
        {
            return new Ray(_camera.transform.position, _camera.transform.forward);
        }
    }
}
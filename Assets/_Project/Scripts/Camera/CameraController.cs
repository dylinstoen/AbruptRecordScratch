using UnityEngine;

namespace FPS.Player {
    public class CameraController : MonoBehaviour, IAimSource
    {        
        [SerializeField] private Camera cam;
        private Transform followTarget;
        private Transform lookAtTarget;
        [SerializeField] private float followSpeed = 1.0f;

        public void Inject(Transform followTarget, Transform lookAtTarget) {
            this.followTarget = followTarget;
            this.lookAtTarget = lookAtTarget;
        }

        public void FollowTarget() {
            transform.position = followTarget.position;
        }

        public void LookAtTarget() {
            Vector3 direction = lookAtTarget.position - transform.position;
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
            transform.LookAt(lookAtTarget);
        }
        private void LateUpdate() {
            FollowTarget();
            LookAtTarget();
        }
        public Transform CamTransform => transform;
        public Vector3 Forward => cam.transform.forward;
        public Vector3 Position => cam.transform.position;
    }
}


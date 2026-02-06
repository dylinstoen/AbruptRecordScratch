using UnityEngine;
using FPS.Aiming;

namespace FPS.Camera {
    public class Controller : MonoBehaviour, IAimSource
    {        
        [SerializeField] private UnityEngine.Camera cam;
        private Transform followTarget;
        private Transform lookAtTarget;
        [SerializeField] private float followSpeed = 1.0f;

        public void Init(Transform followTarget, Transform lookAtTarget) {
            this.followTarget = followTarget;
            this.lookAtTarget = lookAtTarget;
        }

        public void FollowTarget() {
            transform.position = followTarget.position;
        }

        public void LookAtTarget() {
            Vector3 direction = lookAtTarget.position - transform.position;
            if (direction.sqrMagnitude > 0.01f) {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, followSpeed * Time.deltaTime);
            }
        }
        public void LateTick() {
            FollowTarget();
            LookAtTarget();
        }
        public Transform CamTransform => transform;
        public Vector3 Forward => cam.transform.forward;
        public Vector3 Position => cam.transform.position;
    }
}


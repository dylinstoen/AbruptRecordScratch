using UnityEngine;
using FPS.Aiming;
namespace FPS.Weapon {
    public class View : MonoBehaviour {
        [Header("Muzzle Flash")]
        public GameObject muzzleFlash;
        [Header("Recoil")]
        public float horizontalForce = 10f;
        public float verticalForce = 10f;
        [Header("Aim")]
        [SerializeField] private float forwardOffset = 0.5f;
        [SerializeField] private float horizontalOffset = 0.25f;
        [SerializeField] private float verticalOffset = -0.2f;
        [SubHeader("Smoothing")]
        [SerializeField] private float positionSmoothTime = 0.05f;
        [SerializeField] private float rotationSmoothTime = 0.05f;
        private Vector3 positionVelocity;
        private IAimSource aimSource;
        public void Inject(IAimSource source) {
            aimSource = source;
        }
        private void LateUpdate() {
            if (aimSource == null) return;
            Vector3 pos = aimSource.Position + aimSource.Forward * forwardOffset + GetRight() * horizontalOffset + GetUp() * verticalOffset;
            transform.position = Vector3.SmoothDamp(transform.position, pos, ref positionVelocity, positionSmoothTime);
            Quaternion rotationTarget = Quaternion.LookRotation(aimSource.Forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime/rotationSmoothTime);
        }

        private Vector3 GetRight() {
            return Vector3.Cross(Vector3.up, aimSource.Forward).normalized;
        }

        private Vector3 GetUp() {
            Vector3 right = GetRight();
            return Vector3.Cross(aimSource.Forward, right).normalized;
        }

        public void OnFired() {
            // TODO: Play gun fired sound effect, muzzle flash, and apply a visual recoil based on weapon data
        }
    }
}


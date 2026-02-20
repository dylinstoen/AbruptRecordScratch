using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public class WeaponAimView : MonoBehaviour {
        [Header("Aim")]
        [SerializeField] private float forwardOffset = 0.5f;
        [SerializeField] private float horizontalOffset = 0.25f;
        [SerializeField] private float verticalOffset = -0.2f;
        [SubHeader("Smoothing")]
        [SerializeField] private float positionSmoothTime = 0.05f;
        [SerializeField] private float rotationSmoothTime = 0.05f;
        private Vector3 _positionVelocity;

        public void LateTick(in WeaponUseContext ctx) {
            Vector3 aimSourceForward = ctx.AimRay.direction;
            Vector3 aimSourcePosition = ctx.AimRay.origin;
            Vector3 pos = aimSourcePosition + aimSourceForward * forwardOffset + GetRight(aimSourceForward) * horizontalOffset + GetUp(aimSourceForward) * verticalOffset;
            transform.position = Vector3.SmoothDamp(transform.position, pos, ref _positionVelocity, positionSmoothTime);
            Quaternion rotationTarget = Quaternion.LookRotation(aimSourceForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime/rotationSmoothTime);
        }
        private Vector3 GetRight(Vector3 aimSourceForward) {
            return Vector3.Cross(Vector3.up, aimSourceForward).normalized;
        }

        private Vector3 GetUp(Vector3 aimSourceForward) {
            Vector3 right = GetRight(aimSourceForward);
            return Vector3.Cross(aimSourceForward, right).normalized;
        }
    }
}
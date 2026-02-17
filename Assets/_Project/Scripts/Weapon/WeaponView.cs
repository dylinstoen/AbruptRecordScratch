using System;
using _Project.Scripts.Actors;
using _Project.Scripts.Actors.Structs;
using UnityEngine;
using _Project.Scripts.Weapon.Stucts;
namespace _Project.Scripts.Weapon {
    public sealed  class WeaponView : MonoBehaviour {
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

        private Vector3 _positionVelocity;
        private IAimRaySource _aimRaySource;

        public void Initialize(IAimRaySource aimRaySource) {
            _aimRaySource = aimRaySource;
        }
        public void LateTick(float dt) {
            Vector3 aimSourceForward = _aimRaySource.GetAimRay().direction;
            Vector3 aimSourcePosition = _aimRaySource.GetAimRay().origin;
            Vector3 pos = aimSourcePosition + aimSourceForward * forwardOffset + GetRight(aimSourceForward) * horizontalOffset + GetUp(aimSourceForward) * verticalOffset;
            transform.position = Vector3.SmoothDamp(transform.position, pos, ref _positionVelocity, positionSmoothTime);
            Quaternion rotationTarget = Quaternion.LookRotation(aimSourceForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotationTarget, Time.deltaTime/rotationSmoothTime);
        }
        
        public void SetActive(bool active) => gameObject.SetActive(active);
        
        private Vector3 GetRight(Vector3 aimSourceForward) {
            return Vector3.Cross(Vector3.up, aimSourceForward).normalized;
        }

        private Vector3 GetUp(Vector3 aimSourceForward) {
            Vector3 right = GetRight(aimSourceForward);
            return Vector3.Cross(aimSourceForward, right).normalized;
        }

        public void OnFired() {
            // TODO: Play gun fired sound effect, muzzle flash, and apply a visual recoil based on weapon data
        }
    }
}


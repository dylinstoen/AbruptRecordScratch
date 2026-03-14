using System;
using System.Numerics;
using _Project.Scripts.Input;
using KinematicCharacterController;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

namespace _Project.Scripts.Actors {
    public class PlayerMover : MonoBehaviour, ICharacterController {
        [SerializeField] private float walkSpeed = 15f;
        [SerializeField] private float gravity = -90f;
        [SerializeField] private float movementResponsiveness = 25f;
        private IAimRaySource _aimRaySource;
        private IIntentSource _intent;
        [SerializeField] private KinematicCharacterMotor motor;

        public void Initialize(IIntentSource intent, IAimRaySource  aimRaySource) {
            _aimRaySource = aimRaySource;
            _intent = intent;
        }

        private void Awake() {
            motor.CharacterController = this;
        }

        public void UpdateRotation(ref Quaternion currentRotation, float deltaTime) {
            var requestedRotation = _aimRaySource.GetAimRay().direction;
            var forward = Vector3.ProjectOnPlane(requestedRotation, motor.CharacterUp);
            if (forward != Vector3.zero) {
                currentRotation = Quaternion.LookRotation(forward, motor.CharacterUp);
            }
            
        }

        public void UpdateVelocity(ref Vector3 currentVelocity, float deltaTime) {
            if (motor.GroundingStatus.IsStableOnGround) {
                var requestedMovement = new Vector3(_intent.Current.Move.x, 0f, _intent.Current.Move.y);
                requestedMovement = Vector3.ClampMagnitude(requestedMovement, 1f);
                var rotation = Quaternion.LookRotation(_aimRaySource.GetAimRay().direction, motor.CharacterUp);
                requestedMovement = rotation * requestedMovement;
                var groundedMovement = motor.GetDirectionTangentToSurface(requestedMovement, motor.GroundingStatus.GroundNormal) * requestedMovement.magnitude;
                var targetVelocity = groundedMovement * walkSpeed;
                currentVelocity = Vector3.Lerp(currentVelocity, targetVelocity, 1f - Mathf.Exp(-movementResponsiveness * deltaTime));
            }
            else {
                currentVelocity += motor.CharacterUp * (gravity * deltaTime);
            }

        }

        public void BeforeCharacterUpdate(float deltaTime) { }

        public void PostGroundingUpdate(float deltaTime) { }

        public void AfterCharacterUpdate(float deltaTime) { }

        public bool IsColliderValidForCollisions(Collider coll) { return true; }

        public void OnGroundHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, ref HitStabilityReport hitStabilityReport) { }

        public void OnMovementHit(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, 
            ref HitStabilityReport hitStabilityReport) { }

        public void ProcessHitStabilityReport(Collider hitCollider, Vector3 hitNormal, Vector3 hitPoint, Vector3 atCharacterPosition,
            Quaternion atCharacterRotation, ref HitStabilityReport hitStabilityReport) { }

        public void OnDiscreteCollisionDetected(Collider hitCollider) { }
    }
}
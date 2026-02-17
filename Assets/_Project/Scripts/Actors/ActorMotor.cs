using System;
using UnityEngine;
using _Project.Input;
using UnityEngine.InputSystem;
namespace _Project.Scripts.Actors {
    public sealed class ActorMotor : MonoBehaviour {
        [Header("Rigidbody")]
        [SerializeField] private Rigidbody rb;
        [Header("Movement")]
        [SerializeField] private float speed = 10f;
        private IAimRaySource _aimRaySource;
        private IIntentSource _intent;
        private void Start() {
            rb = GetComponent<Rigidbody>();
 
        }

        public void Initialize(IIntentSource intent) {
            _intent = intent;
        }

        public void BindAimSource(IAimRaySource aimRaySource) {
            _aimRaySource = aimRaySource;
        }
        /// <summary>
        /// Moves the player based on the local forward of the moveReference gameobject target
        /// </summary>
        private void FixedUpdate() {
            Vector3 forward = _aimRaySource.GetAimRay().direction;
            Vector2 direction = _intent.Current.Move;
            Vector3 right = Vector3.Cross(Vector3.up, forward);
            forward.y = 0f;
            right.y = 0f;
            forward.Normalize();
            right.Normalize();
            Vector3 moveDir = forward * direction.y + right * direction.x;
            rb.linearVelocity = moveDir * speed + new Vector3(0, rb.linearVelocity.y, 0);
        }
    }
}


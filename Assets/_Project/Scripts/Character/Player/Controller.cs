using System;
using UnityEngine;
using FPS.Input;
using UnityEngine.InputSystem;
using PlayerInput = FPS.Input.PlayerInput;
using FPS.Aiming;
namespace FPS.Character.Player {
    public class Controller : MonoBehaviour {
        [Header("Rigidbody")]
        [SerializeField] private Rigidbody rb;
        [Header("Input")]
        [SerializeField] private IInput _input;
        [Header("Movement")]
        [SerializeField] private float speed = 10f;
        [Header("Look")]
        [SerializeField] private Transform lookTarget;
        [SerializeField] private float yMinLimit = -80f;
        [SerializeField] private float yMaxLimit = 80f;
        [SerializeField] float sensitivity = 10f;
        [SerializeField] private float distance = 5f;
        private float pitch = 0f;
        private float yaw = 0f;
        private IAimSource aimSource;
        private void Start() {
            _input = GetComponent<PlayerInput>();
            rb = GetComponent<Rigidbody>();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        public void Tick() {
            PerformLook();
            PerformPause();
        }
        public void FixedTick() {
            PerformMove();
        }
        void PerformPause() {
            if (_input.Pause()) Cursor.visible = !Cursor.visible;
        }

        public void Init(IAimSource aimSource) {
            this.aimSource = aimSource;
        }
        /// <summary>
        /// Projects a LookReference gameobject target out in front of the player based on the mouse movement
        /// </summary>
        void PerformLook() {
            Vector2 delta = _input.Look();
            pitch += delta.x * sensitivity * 0.01f;
            yaw -= delta.y * sensitivity * 0.01f;
            yaw = Mathf.Clamp(yaw, yMinLimit, yMaxLimit);
            Quaternion newLookRotation = Quaternion.Euler(yaw, pitch, 0f);
            Vector3 newVector = newLookRotation * Vector3.forward;
            lookTarget.position = transform.position + newVector * distance;
        }
        /// <summary>
        /// Moves the player based on the local forward of the moveReference gameobject target
        /// </summary>
        void PerformMove() {
            if (aimSource == null) return;
            var direction = _input.Move();
            Vector3 forward = aimSource.Forward;
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


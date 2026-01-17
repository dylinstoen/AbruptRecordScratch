using System;
using UnityEngine;
using FPS.Input;
using UnityEngine.InputSystem;

namespace FPS.Player {
    public class PlayerMotor : MonoBehaviour {
        [Header("Rigidbody")]
        [SerializeField] private Rigidbody rb;
        [Header("Input")]
        [SerializeField] private IInputReader inputReader;
        [Header("Movement")]
        [SerializeField] private Transform moveReference;
        [SerializeField] private float speed = 10f;
        [Header("Look")]
        [SerializeField] private Transform lookReference;
        [SerializeField] private float yMinLimit = -80f;
        [SerializeField] private float yMaxLimit = 80f;
        [SerializeField] float sensitivity = 10f;
        [SerializeField] private float distance = 5f;
        private float pitch = 0f;
        private float yaw = 0f;
        
        /// <summary>
        /// Injects a moveReference game object target into the player so it can move locally forward based on the cam
        /// </summary>
        /// <param name="moveReference"></param>
        public void Initialize(Transform moveReference) {
            this.moveReference = moveReference;
        }
        private void Start() {
            inputReader = GetComponent<PlayerInputReader>();
            rb = GetComponent<Rigidbody>();
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        private void Update() {
            PerformLook();
            PerformPause();
        }
        private void FixedUpdate() {
            PerformMove();
        }
        void PerformPause() {
            if (inputReader.Pause()) Cursor.visible = !Cursor.visible;
        }
        
        /// <summary>
        /// Projects a LookReference gameobject target out in front of the player based on the mouse movement
        /// </summary>
        void PerformLook() {
            Vector2 delta = inputReader.Look();
            pitch += delta.x * sensitivity * 0.01f;
            yaw -= delta.y * sensitivity * 0.01f;
            yaw = Mathf.Clamp(yaw, yMinLimit, yMaxLimit);
            Quaternion newLookRotation = Quaternion.Euler(yaw, pitch, 0f);
            Vector3 newVector = newLookRotation * Vector3.forward;
            lookReference.position = transform.position + newVector * distance;
        }
        /// <summary>
        /// Moves the player based on the local forward of the moveReference gameobject target
        /// </summary>
        void PerformMove() {
            var direction = inputReader.Move();
            Vector3 refForward = moveReference.forward;
            Vector3 refRight = moveReference.right;
            refForward.y = 0f;
            refRight.y = 0f;
            refForward.Normalize();
            refRight.Normalize();
            Vector3 moveDir = refForward * direction.y + refRight * direction.x;
            rb.MovePosition(rb.position + moveDir * (speed * Time.fixedDeltaTime));
        }
    }
}


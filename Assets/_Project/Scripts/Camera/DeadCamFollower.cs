using System;
using UnityEngine;

namespace _Project.Scripts.Cam {
    public class DeadCamFollower : MonoBehaviour{
        private Transform _target;
        [SerializeField] private Vector3 posOffset = new Vector3(0f, -0.67f, 0f);
        [SerializeField] private float rotationOffset = -13.02f;
        public void SetTarget(Transform target) {
            _target = target;
        }

        private void Update() {
            if (!_target)
                return;
            transform.position = _target.position + posOffset;
            transform.rotation = Quaternion.Euler(0f, _target.rotation.eulerAngles.y, rotationOffset);
        }
    }
}
using System;
using Unity.Cinemachine;
using UnityEngine;

namespace _Project.Scripts.Cam {
    public class CameraTargetSource : MonoBehaviour {
        [SerializeField] private Transform deadTarget;
        [SerializeField] private CinemachineCamera cinemachineCamera;
        private void Update() {
            if (!cinemachineCamera.Follow) {
                cinemachineCamera.Follow = deadTarget;
            }
        }
    }
}
using FPS;
using UnityEngine;
using FPS.Player;

public class GameSpawner : MonoBehaviour {
    [SerializeField] private PlayerMotor playerMotor;
    [SerializeField] private Transform cameraFollowTarget;
    [SerializeField] private Transform cameraLookTarget;
    [SerializeField] private CameraController cameraController;

    private void Start() {
        playerMotor.Inject(cameraController);
        cameraController.Inject(cameraFollowTarget, cameraLookTarget);
    }
}

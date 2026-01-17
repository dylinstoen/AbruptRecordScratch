using UnityEngine;
using FPS.Player;
using FPS.Camera;

public class GameSpawner : MonoBehaviour {
    [SerializeField] private MonoBehaviour cameraControllerSource;
    [SerializeField] private Transform cameraFollowTarget;
    [SerializeField] private Transform cameraLookTarget;
    [SerializeField] private PlayerMotor playerMotor;

    private ICameraController cameraController;
    
    private void Awake() {
        cameraController = cameraControllerSource as ICameraController;
        if(cameraController == null) Debug.LogError($"{cameraControllerSource.name} does not implement ICameraController");
    }

    private void Start() {
        playerMotor.Initialize(cameraController.CamTransform);
        cameraController.Initialize(cameraFollowTarget, cameraLookTarget);
    }
}

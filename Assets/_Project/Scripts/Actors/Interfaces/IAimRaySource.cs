using UnityEngine;

namespace _Project.Scripts.Actors {
    /// <summary>
    /// Handles where the actor is aiming and is used for internal weapon systems (different from ILookCameraSource so you can seperate weapon aiming and where the player is looking)
    /// </summary>
    public interface IAimRaySource {
        Ray GetAimRay();
    }
}


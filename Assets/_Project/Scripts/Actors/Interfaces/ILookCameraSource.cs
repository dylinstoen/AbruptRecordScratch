namespace _Project.Scripts.Actors {
    using UnityEngine;
    /// <summary>
    /// Where the actual actor or player is looking
    /// </summary>
    public interface ILookCameraSource {
        public void SetLookInput(Vector2 lookInput);
    }
}
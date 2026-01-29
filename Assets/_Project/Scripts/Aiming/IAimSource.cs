using UnityEngine;

namespace FPS.Aiming {
    public interface IAimSource {
        Vector3 Forward { get; }
        Vector3 Position { get; }
    }
}


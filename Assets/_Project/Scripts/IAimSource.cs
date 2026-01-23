using UnityEngine;

namespace FPS {
    public interface IAimSource {
        Vector3 Forward { get; }
        Vector3 Position { get; }
    }
}


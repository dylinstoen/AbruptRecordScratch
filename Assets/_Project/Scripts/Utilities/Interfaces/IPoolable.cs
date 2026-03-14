using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public interface IPoolable {
        bool InUse { get; }
        void Bind(Action<Component> returnToPool);
        void MarkInUse();
        void MarkFree();
        void ForceRecycle();
    }
}
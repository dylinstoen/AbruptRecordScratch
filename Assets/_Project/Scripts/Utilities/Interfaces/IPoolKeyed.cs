using System;
using UnityEngine;

namespace _Project.Scripts.Utilities {
    public interface IPoolKeyed<TKey> {
        bool InUse { get; }
        void Bind(Action<Component, TKey> returnToPool, TKey key);
        void MarkInUse();
        void MarkFree();
        void ForceRecycle();
    }
}
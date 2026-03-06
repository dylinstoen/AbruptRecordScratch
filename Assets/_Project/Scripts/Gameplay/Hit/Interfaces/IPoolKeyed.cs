using System;
using UnityEngine;

namespace _Project.Scripts.Gameplay {
    public interface IPoolKeyed<TEnum> where TEnum : unmanaged, Enum {
        bool InUse { get; }
        void Bind(Action<Component, TEnum> returnToPool, TEnum key);
        void MarkInUse();
        void MarkFree();
        void ForceRecycle();
    }
}
using System;

namespace _Project.Scripts.Actors {
    public interface IHealthEvents {
        event Action<int, int> HealthChanged;
    }
}
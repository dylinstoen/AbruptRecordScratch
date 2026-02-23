using System;

namespace _Project.Scripts.Actors {
    public interface IDeathEvents {
        public event Action Died;
    }
}
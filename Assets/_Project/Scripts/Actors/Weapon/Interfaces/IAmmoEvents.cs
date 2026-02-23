using System;

namespace _Project.Scripts.Actors {
    public interface IAmmoEvents {
        public event Action<int, int> OnAmmoChanged;
        public void Refresh();
    }
}
using System;

namespace _Project.Scripts.Actors {
    public interface IAmmoEvents {
        public event Action<int, int, bool> OnAmmoChanged;
        public void Refresh();
    }
}
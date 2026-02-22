using System;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Enums;

namespace _Project.Scripts.Actors {
    public interface IAmmoInventory {
        public event Action<AmmoType, int> OnCurrentAmmoChange;
        int GetCurrent(AmmoType  type);
        int GetMax(AmmoType  type);
        int StoreUpToMax(AmmoType  type, int requested);
        void SetMax(AmmoType type, int max);
        int ConsumeUpTo(AmmoType type, int requested);
    }
}
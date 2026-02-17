using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Enums;

namespace _Project.Scripts.Actors {
    public interface IAmmoInventory {
        int GetCurrent(AmmoType  type);
        int GetMax(AmmoType  type);
        void SetMax(AmmoType type, int max);
        void Add(AmmoType type, int amount);
        // Gets how much the actor can carry in reserve
        bool TryConsume(AmmoType type, int amount);
    }
}
using _Project.Scripts.Weapon.Enums;

namespace _Project.Scripts.Actors {
    public interface IAmmoAcquirer {
        bool TryAddAmmo(AmmoType type, int amount);
    }
}
using _Project.Scripts.Weapon;

namespace _Project.Scripts.Actors {
    public interface IWeaponAcquirer {
        bool TryAddWeapon(WeaponSO weaponSo);
    }
}
using System;
using _Project.Scripts.Weapon.Enums;

namespace _Project.Scripts.Weapon {
    public interface IWeaponAmmoView {
        AmmoType AmmoType { get; }
        int Mag { get; }
        int Reserve { get; }
        bool infiniteAmmo { get; }
        event Action AmmoChanged;
    }
}
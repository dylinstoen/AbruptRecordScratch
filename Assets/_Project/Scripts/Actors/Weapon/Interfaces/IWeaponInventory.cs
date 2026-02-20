using System;
using _Project.Scripts.Weapon;

namespace _Project.Scripts.Actors {
    public interface IWeaponInventory {
        public event Action<IWeaponAmmoView> OnWeaponChanged;
        IWeapon CurrentWeapon { get; }
        void NextWeapon();
        void PreviousWeapon();
        void Equip(IWeapon weapon);
    }
}
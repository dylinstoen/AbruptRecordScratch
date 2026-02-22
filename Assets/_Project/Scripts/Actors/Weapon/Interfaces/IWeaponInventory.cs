using System;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Stucts;

namespace _Project.Scripts.Actors {
    public interface IWeaponInventory {
        public event Action<WeaponFacets> OnWeaponChanged;
        WeaponFacets CurrentWeapon { get; }
        void NextWeapon();
        void PreviousWeapon();
        bool TryEquip(WeaponFacets weapon);
    }
}
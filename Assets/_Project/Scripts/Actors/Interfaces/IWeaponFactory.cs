using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Weapon;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public interface IWeaponFactory {
        public IWeapon Create(WeaponDefinition weaponDefinition, IAmmoInventory ammoInventory, WeaponDeps weaponDeps);
    }
}
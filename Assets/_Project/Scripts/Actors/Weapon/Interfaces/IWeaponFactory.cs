using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Weapon;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public interface IWeaponFactory {
        public IWeapon Create(WeaponSO weaponSo, WeaponDeps weaponDeps);
    }
}
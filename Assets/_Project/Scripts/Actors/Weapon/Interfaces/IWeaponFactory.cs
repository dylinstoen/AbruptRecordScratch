using _Project.Scripts.Actors.Structs;
using _Project.Scripts.Weapon;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Actors {
    public interface IWeaponFactory {
        public WeaponFacets Create(WeaponSO weaponSo, WeaponDeps weaponDeps);
    }
}
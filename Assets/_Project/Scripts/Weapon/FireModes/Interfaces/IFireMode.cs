using System;
using _Project.Scripts.Weapon.Stucts;

namespace _Project.Scripts.Weapon {
    public interface IFireMode {
        event Action DryFired;
        event Action ShotFired;
        void Equip();
        void Unequip();
        void StartFire(WeaponUseContext ctx);
        void StopFire(WeaponUseContext ctx);
        void Tick(WeaponUseContext ctx);
    }
}
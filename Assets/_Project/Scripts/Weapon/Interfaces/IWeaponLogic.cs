using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public interface IWeaponLogic {
        void Tick(in WeaponUseContext ctx);
        void StartFire(in WeaponUseContext ctx);
        void StopFire(in WeaponUseContext ctx);
        void LateTick(in WeaponUseContext ctx);
    }
}
using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Stucts;
using UnityEngine;

namespace _Project.Scripts.Weapon {
    public interface IWeapon: IWeaponAmmoView {
        void Tick(in WeaponUseContext ctx);
        void StartFire(in WeaponUseContext ctx);
        void StopFire(in WeaponUseContext ctx);
        void OnUnequip();
        void OnEquip();
        void SetActive(bool active);
        void LateTick(in WeaponUseContext ctx);
        void OnCreate();
        void Dispose();
    }
}
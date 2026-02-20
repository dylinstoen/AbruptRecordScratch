using _Project.Scripts.Actors;
using _Project.Scripts.Weapon.Stucts;

namespace _Project.Scripts.Weapon {
    public interface IWeaponStateController {
        void Tick(in WeaponUseContext ctx);
        void StartFire(in WeaponUseContext ctx);
        void StopFire(in WeaponUseContext ctx);
        void RequestReload();
        void OnEquip();
        void OnCreate();
        void Dispose();
        void OnUnequip();
    }
}